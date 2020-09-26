#if REF_ONLINE_AUTH && REF_FIREBASE_AUTH && REF_USE_FIREBASE

using UnityEngine;

using REF.Runtime.Online.Service;
using REF.Runtime.Diagnostic;

using Firebase.Auth;
using Firebase.Extensions;

using FirebaseCredential = Firebase.Auth.Credential;

namespace REF.Runtime.Online.Auth
{
	[CreateAssetMenu(fileName = "FirebaseAuthService", menuName = "REF/Online/Auth/Firebase Auth")]
	public class FirebaseAuthService : FirebaseService, IAuthService
	{
		public event System.Action OnTokenChanged;

		[SerializeField] private User internalUser;
		private FirebaseUser internalFirebaseUser;
		private bool isSignedIn = false;

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
				internalUser.SetPhotoUri(update.PhotoUri);
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
					internalUser.SetPhotoUri(update.PhotoUri);
					internalUser.SetDisplayName(update.DisplayName);
					OnSuccess?.Invoke();
				}
				else
				{
					this.Log("Failed to update user profile: {0}", task.Exception.Message);
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
				this.Log("You didn't initialize auth service or you aren't logged in!");
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
						internalUser?.SetProvider(credential.GetProviderId());
						RequestTokenInternal(internalFirebaseUser, OnLinked);
					}
					else
					{
						this.Log("Failed to link account: {0}", task.Exception.Message);
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
				this.Log("You didn't initialize auth service or you aren't logged in!");
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

						internalUser?.SetProvider(credential.GetProviderId());
						RequestTokenInternal(internalFirebaseUser, (user) => { OnSucess?.Invoke(); });
					}
					else
					{
						this.Log("Failed to re-authenticate: {0}", task.Exception.Message);
						OnFailed?.Invoke();
					}
				});
			}
			else
			{
				this.Log("Failed to re-authenticate, because credential is null!");
				OnFailed?.Invoke();
			}
		}

		public void SignUp(Credential credential, System.Action<User> OnSignedIn = null, System.Action OnFailed = null)
		{
			if (!IsInitialized())
			{
				this.Log("You didn't initialize auth service");
				OnFailed?.Invoke();
				return;
			}

			if (IsSignedIn())
			{
				this.Log("You already signed in!");
				OnFailed?.Invoke();
				return;
			}

			var provider = credential.GetProviderId();
			var auth = FirebaseAuth.DefaultInstance;

			if (provider == "EmailPassword")
			{
				auth.CreateUserWithEmailAndPasswordAsync(credential.GetEmail(), credential.GetPassword()).ContinueWithOnMainThread((creationTask) =>
				{
					if (creationTask.IsCompleted && creationTask.Exception == null)
					{
						isSignedIn = true;
						internalUser?.SetProvider(credential.GetProviderId());
						RequestTokenInternal(creationTask.Result, OnSignedIn);
					}
					else
					{
						this.Log("Failed to create a user: {0}", creationTask.Exception.Message);
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
				this.Log("You didn't initialize auth service");
				OnFailed?.Invoke();
				return;
			}

			if (IsSignedIn())
			{
				this.Log("You already signed in!");
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
				auth.StateChanged += OnAuthStateChangedHandler;
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
					this.Log("Failed to request user token: {0}", task.Exception.Message);
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
						internalUser?.SetProvider(credential.GetProviderId());
						RequestTokenInternal(internalFirebaseUser, OnSignedIn);
					}
					else
					{
						this.Log("Failed to sign-in: {0}", task.Exception.Message);
						OnFailed?.Invoke();
					}
				});
			}
			else
			{
				var provider = credential.GetProviderId();

				switch (provider)
				{
					case "Anonymouse":
					{
						auth.SignInAnonymouslyAsync().ContinueWithOnMainThread((task) =>
						{
							if (task.IsCompleted && task.Exception == null)
							{
								isSignedIn = true;
								internalUser?.SetProvider(credential.GetProviderId());
								RequestTokenInternal(internalFirebaseUser, OnSignedIn);
							}
							else
							{
								this.Log("Failed to sign-in: {0}", task.Exception.Message);
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
								internalUser?.SetProvider(credential.GetProviderId());
								RequestTokenInternal(internalFirebaseUser, OnSignedIn);
							}
							else
							{
								this.Log("Failed to sign-in: {0}", task.Exception.Message);
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
				RequestTokenInternal(internalFirebaseUser, (user) => { OnTokenChanged?.Invoke(); }, true);
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
			user.SetPhotoUri(firebaseUser.PhotoUrl);
			user.SetUid(firebaseUser.UserId);
			user.SetProvider(firebaseUser.ProviderId);

			return user;
		}

		private FirebaseCredential ToFirebaseCredential(Credential credential)
		{
			var provider = credential.GetProviderId();

			switch (provider)
			{
				case "EmailPassword":
				{
					var creds = EmailAuthProvider.GetCredential(credential.GetEmail(), credential.GetPassword());
					return creds;
				}

				case "facebook.com":
				{
					var creds = FacebookAuthProvider.GetCredential(credential.GetToken());
					return creds;
				}

				case "Apple":
				{
					var creds = OAuthProvider.GetCredential("apple.com", credential.GetToken(), credential.GetNonce(), null);
					return creds;
				}
			}

			return null;
		}

#if UNITY_EDITOR
		[ContextMenu("Sign Out")]
		private void SignOutEditor()
		{
			SignOut();
		}
#endif
	}
}

#endif