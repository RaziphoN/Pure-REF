#if REF_ONLINE_AUTH && REF_FIREBASE_AUTH && REF_USE_FIREBASE

using UnityEngine;
using UnityEngine.Networking;

using System.Linq;

using REF.Runtime.Online.Service;
using REF.Runtime.Diagnostic;

using Firebase.Auth;
using Firebase.Extensions;

using FirebaseCredential = Firebase.Auth.Credential;

namespace REF.Runtime.Online.Auth
{
	// TODO: Make a config file to match provider ids
	[System.Serializable]
	public class FirebaseAuthService : FirebaseService, IAuthService
	{
		public event System.Action OnTokenChanged;

		[SerializeField] private User internalUser;
		[SerializeField] private bool isSignedIn = false;
		private FirebaseUser internalFirebaseUser;

		public bool IsSignedIn()
		{
			return isSignedIn;
		}

		public void ChangeUserInfo(UserUpdate update, System.Action OnSuccess = null, System.Action OnFail = null)
		{
			if (!IsInitialized() || !IsSignedIn())
			{
				OnFail?.Invoke();
				return;
			}

			var profile = new UserProfile();
			profile.DisplayName = string.IsNullOrEmpty(update.DisplayName) ? null : update.DisplayName;
			profile.PhotoUrl = update.PhotoUri;

			if (update.PhotoUri != null)
			{
				internalUser.SetPhotoUrl(update.PhotoUri);
			}

			if (!string.IsNullOrEmpty(update.DisplayName))
			{
				internalUser.SetDisplayName(update.DisplayName);
			}

			if (!string.IsNullOrEmpty(update.Email))
			{
				internalUser.SetEmail(update.Email);
			}

			internalFirebaseUser.UpdateUserProfileAsync(profile).ContinueWithOnMainThread((task) =>
			{
				if (task.IsCompleted && task.Exception == null)
				{
					internalUser.SetPhotoUrl(update.PhotoUri);
					internalUser.SetDisplayName(update.DisplayName);
					OnSuccess?.Invoke();
				}
				else
				{
					HandlerError(task.Exception, "Failed to update user profile!");
					OnFail?.Invoke();
				}
			});

			if (update.Email != null)
			{
				internalFirebaseUser.UpdateEmailAsync(update.Email);
			}
		}

		public User GetUser()
		{
			return internalUser;
		}

		public void Link(Credential credential, System.Action<User> OnLinked = null, System.Action OnFailed = null)
		{
			if (!IsInitialized() || !IsSignedIn())
			{
				RefDebug.Error(nameof(FirebaseAuthService), "You didn't initialize auth service or you aren't logged in!");
				OnFailed?.Invoke();
				return;
			}

			var creds = ToFirebaseCredential(credential);

			if (creds != null)
			{
				internalFirebaseUser.LinkWithCredentialAsync(creds).ContinueWithOnMainThread((task) =>
				{
					if (task.IsCompleted && task.Exception == null)
					{
						internalFirebaseUser = task.Result;
						OnPostAuthHandler(credential, OnLinked);
					}
					else
					{
						HandlerError(task.Exception, "Failed to link account!");
						OnFailed?.Invoke();
					}
				});
			}
			else
			{
				OnFailed?.Invoke();
			}
		}

		public void ReAuth(Credential credential, System.Action OnSucess = null, System.Action OnFailed = null)
		{
			if (!IsInitialized() || !IsSignedIn())
			{
				RefDebug.Error(nameof(FirebaseAuthService), "You didn't initialize auth service or you aren't logged in!");
				OnFailed?.Invoke();
				return;
			}

			var creds = ToFirebaseCredential(credential);

			if (creds != null)
			{
				internalFirebaseUser.ReauthenticateAndRetrieveDataAsync(creds).ContinueWithOnMainThread((task) =>
				{
					if (task.IsCompleted && task.Exception == null)
					{
						internalUser.SetData(null);

						foreach (var obj in task.Result.Info.Profile)
						{
							internalUser?.SetKey(obj.Key, obj.Value.ToString());
						}

						OnPostAuthHandler(credential, (user) => { OnSucess?.Invoke(); });
					}
					else
					{
						HandlerError(task.Exception, "Failed to re-auth");
						OnFailed?.Invoke();
					}
				});
			}
			else
			{
				RefDebug.Error(nameof(FirebaseAuthService), "Failed to re-authenticate, because credential is null!");
				OnFailed?.Invoke();
			}
		}

		public void SignUp(Credential credential, System.Action<User> OnSignedIn = null, System.Action OnFailed = null)
		{
			if (!IsInitialized())
			{
				RefDebug.Error(nameof(FirebaseAuthService), "You didn't initialize auth service");
				OnFailed?.Invoke();
				return;
			}

			if (IsSignedIn())
			{
				RefDebug.Error(nameof(FirebaseAuthService), "You already signed in!");
				OnFailed?.Invoke();
				return;
			}

			var provider = credential.GetProviderId();
			var auth = FirebaseAuth.DefaultInstance;

			if (provider == "password")
			{
				auth.CreateUserWithEmailAndPasswordAsync(credential.GetEmail(), credential.GetPassword()).ContinueWithOnMainThread((creationTask) =>
				{
					if (creationTask.IsCompleted && creationTask.Exception == null)
					{
						isSignedIn = true;
						OnPostAuthHandler(credential, OnSignedIn);
					}
					else
					{
						HandlerError(creationTask.Exception, "Failed to create a user");
						OnFailed?.Invoke();
					}
				});
			}
			else
			{
				SignInInternal(credential, OnSignedIn, OnFailed);
			}
		}

		public void SignIn(Credential credential, System.Action<User> OnSignedIn = null, System.Action OnFailed = null)
		{
			if (!IsInitialized())
			{
				RefDebug.Error(nameof(FirebaseAuthService), "You didn't initialize auth service");
				OnFailed?.Invoke();
				return;
			}

			if (IsSignedIn())
			{
				RefDebug.Error(nameof(FirebaseAuthService), "You already signed in!");
				OnFailed?.Invoke();
				return;
			}

			SignInInternal(credential, OnSignedIn, OnFailed);
		}

		public void SignOut()
		{
			if (!IsInitialized() || !IsSignedIn())
			{
				return;
			}


			var auth = FirebaseAuth.DefaultInstance;
			auth.SignOut();
			isSignedIn = false;
		}

		public override void Release(System.Action callback)
		{
			var auth = FirebaseAuth.DefaultInstance;
			auth.StateChanged -= OnAuthStateChangedHandler;
			auth.IdTokenChanged -= OnIdTokenChangedHandler;

			base.Release(callback);
		}

		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			if (successful)
			{
				var auth = FirebaseAuth.DefaultInstance;
				auth.StateChanged -= OnAuthStateChangedHandler;
				auth.StateChanged += OnAuthStateChangedHandler;

				auth.IdTokenChanged -= OnIdTokenChangedHandler;
				auth.IdTokenChanged += OnIdTokenChangedHandler;
				OnAuthStateChangedHandler(this, null);
			}

			callback?.Invoke();
		}

		private void RequestTokenInternal(FirebaseUser firebaseUser, System.Action<User> OnComplete, bool refresh = false)
		{
			var user = FromFirebaseUser(firebaseUser);

			firebaseUser.TokenAsync(refresh).ContinueWithOnMainThread((task) =>
			{
				if (task.IsCompleted && task.Exception == null)
				{
					user.SetToken(task.Result);
					internalUser?.SetToken(task.Result);
				}
				else
				{
					HandlerError(task.Exception, "Failed to request user token");
				}

				OnComplete?.Invoke(user);
			});
		}

		private void SignInInternal(Credential credential, System.Action<User> OnSignedIn, System.Action OnFailed)
		{
			var auth = FirebaseAuth.DefaultInstance;
			var firebaseCredential = ToFirebaseCredential(credential);

			if (firebaseCredential != null)
			{
				auth.SignInWithCredentialAsync(firebaseCredential).ContinueWithOnMainThread((task) =>
				{
					if (task.IsCompleted && task.Exception == null)
					{
						isSignedIn = true;
						OnPostAuthHandler(credential, OnSignedIn);
					}
					else
					{
						HandlerError(task.Exception, "Failed to sign-in");
						OnFailed?.Invoke();
					}
				});
			}
			else
			{
				var provider = credential.GetProviderId();

				switch (provider)
				{
					case "anonymouse":
					{
						auth.SignInAnonymouslyAsync().ContinueWithOnMainThread((task) =>
						{
							if (task.IsCompleted && task.Exception == null)
							{
								isSignedIn = true;
								OnPostAuthHandler(credential, OnSignedIn);
							}
							else
							{
								HandlerError(task.Exception, "Failed to sign-in");
								OnFailed?.Invoke();
							}
						});
					}
					break;

					default:
					{
						auth.SignInWithCustomTokenAsync(credential.GetToken()).ContinueWithOnMainThread((task) =>
						{
							if (task.IsCompleted && task.Exception == null)
							{
								isSignedIn = true;
								OnPostAuthHandler(credential, OnSignedIn);
							}
							else
							{
								HandlerError(task.Exception, "Failed to sign-in");
								OnFailed?.Invoke();
							}
						});
					}
					break;
				}
			}
		}

		private void OnIdTokenChangedHandler(object sender, System.EventArgs e)
		{
			if (internalFirebaseUser != null && IsSignedIn() && IsInitialized())
			{

				RequestTokenInternal(internalFirebaseUser, (user) => { OnTokenChanged?.Invoke(); }, false);
			}
		}

		private void OnAuthStateChangedHandler(object sender, System.EventArgs e)
		{
			var auth = FirebaseAuth.DefaultInstance;

			if (auth.CurrentUser != internalFirebaseUser)
			{
				isSignedIn = internalFirebaseUser != auth.CurrentUser && auth.CurrentUser != null;

				if (!isSignedIn && internalFirebaseUser != null)
				{
					internalUser = null;
					internalFirebaseUser = null;
				}

				internalFirebaseUser = auth.CurrentUser;

				if (isSignedIn)
				{
					internalUser = FromFirebaseUser(internalFirebaseUser);
				}
			}
		}

		private User FromFirebaseUser(FirebaseUser firebaseUser)
		{
			var user = new User();
			
			user.SetDisplayName(firebaseUser.DisplayName);
			user.SetEmail(firebaseUser.Email);
			user.SetPhoneNumber(firebaseUser.PhoneNumber);
			user.SetPhotoUrl(firebaseUser.PhotoUrl);
			user.SetUid(firebaseUser.UserId);
			user.SetProvider(firebaseUser.ProviderId);

			foreach (var data in firebaseUser.ProviderData)
			{
				var providerData = new ProviderData(data.ProviderId, data.DisplayName, data.Email, data.PhotoUrl, data.UserId);
				user.AddProviderData(providerData);
			}

			return user;
		}

		private FirebaseCredential ToFirebaseCredential(Credential credential)
		{
			var provider = credential.GetProviderId();

			switch (provider)
			{
				case "password":
				{
					var creds = EmailAuthProvider.GetCredential(credential.GetEmail(), credential.GetPassword());
					return creds;
				}

				case "facebook.com":
				{
					var creds = FacebookAuthProvider.GetCredential(credential.GetToken());
					return creds;
				}

				case "apple.com":
				{
					var creds = OAuthProvider.GetCredential(provider, credential.GetToken(), credential.GetRawNonce(), null);
					return creds;
				}
			}

			return null;
		}

		private void OnPostAuthHandler(Credential credential, System.Action<User> callback)
		{
			// NOTE: Kinda hacky, but f**k this facebook
			if (credential.GetProviderId() == "facebook.com")
			{
				var matchData = internalFirebaseUser.ProviderData.Where((info) => { return info.ProviderId == credential.GetProviderId(); }).ToList();
				var data = matchData.FirstOrDefault();

				if (data != null)
				{
					UnityWebRequest request = UnityWebRequest.Get(data.PhotoUrl + $"?width=200&height=200&access_token={credential.GetToken()}&redirect=false");
					var op = request.SendWebRequest();
					op.completed += (asyncOp) =>
					{
						var wrAsyncOp = (UnityWebRequestAsyncOperation)asyncOp;
						var response = JsonUtility.FromJson<FacebookUrlResponse>(wrAsyncOp.webRequest.downloadHandler.text);
						var changes = new UserProfile();

						changes.DisplayName = data.DisplayName;
						changes.PhotoUrl = new System.Uri(response.data.url);

						internalFirebaseUser.UpdateUserProfileAsync(changes).ContinueWithOnMainThread((avatarTask) =>
						{
							UpdateUserInfo(credential, callback);
						});
					};
				}
				else
				{
					UpdateUserInfo(credential, callback);
				}
			}
			else
			{
				UpdateUserInfo(credential, callback);
			}
		}

		private void UpdateUserInfo(Credential credential, System.Action<User> callback)
		{
			internalUser?.SetProvider(credential.GetProviderId());
			RequestTokenInternal(internalFirebaseUser, callback);
		}

		private void HandlerError(System.AggregateException exception, string message)
		{
			RefDebug.Error(nameof(FirebaseAuthService), $"{message}, exception: {exception.Message}", null);

			if (exception != null)
			{
				foreach (var inner in exception.InnerExceptions)
				{
					RefDebug.Error(nameof(FirebaseAuthService), "Error: {0}", null, inner.Message);
				}
			}
		}

		[System.Serializable]
		private class FacebookUrlResponse
		{
			[System.Serializable]
			public class Data
			{
				public string url = string.Empty;
			}

			public Data data = new Data();
		}
	}
}

#endif