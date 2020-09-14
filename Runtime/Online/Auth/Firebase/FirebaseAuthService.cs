using UnityEngine;

using REF.Runtime.Online.Service;

using Firebase.Auth;
using Firebase.Extensions;

using FirebaseCredential = Firebase.Auth.Credential;

namespace REF.Runtime.Online.Auth
{
	[CreateAssetMenu(fileName = "FirebaseAuthService", menuName = "REF/Online/Auth/Firebase Auth")]
	public class FirebaseAuthService : FirebaseService, IAuthService
	{
		private FirebaseUser internalFirebaseUser;
		private User internalUser;

		public override void Release(System.Action callback)
		{
			var auth = FirebaseAuth.DefaultInstance;
			auth.StateChanged -= OnAuthStateChangedHandler;

			base.Release(callback);
		}

		public bool IsSignedIn()
		{
			return internalUser != null;
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
				OnFailed?.Invoke();
				return;
			}

			if (!credential.IsValid())
			{
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
						RequestTokenInternal(internalFirebaseUser, OnLinked);
					}
					else
					{
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

						OnSucess?.Invoke();
					}
					else
					{
						OnFailed?.Invoke();
					}
				});
			}
			else
			{
				OnFailed?.Invoke();
			}
		}

		public void SignUp(Credential credential, System.Action<User> OnSignedIn = null, System.Action OnFailed = null)
		{
			if (!IsInitialized())
			{
				OnFailed?.Invoke();
				return;
			}

			if (IsSignedIn())
			{
				OnFailed?.Invoke();
				return;
			}

			if (!credential.IsValid())
			{
				OnFailed.Invoke();
				return;
			}

			var provider = credential.GetProvider();
			var auth = FirebaseAuth.DefaultInstance;

			if (provider == ProviderType.EmailPassword)
			{
				auth.CreateUserWithEmailAndPasswordAsync(credential.GetEmail(), credential.GetPassword()).ContinueWithOnMainThread((creationTask) =>
				{
					if (creationTask.IsCompleted && creationTask.Exception == null)
					{
						RequestTokenInternal(creationTask.Result, OnSignedIn);
					}
					else
					{
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
				OnFailed?.Invoke();
				return;
			}

			if (IsSignedIn())
			{
				OnFailed?.Invoke();
				return;
			}

			if (!credential.IsValid())
			{
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
		}

		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			if (successful)
			{
				var auth = FirebaseAuth.DefaultInstance;
				auth.StateChanged += OnAuthStateChangedHandler;
				OnAuthStateChangedHandler(this, null);
			}

			callback?.Invoke();
		}

		private void RequestTokenInternal(FirebaseUser firebaseUser, System.Action<User> OnComplete)
		{
			var user = FromFirebaseUser(firebaseUser);

			firebaseUser.TokenAsync(false).ContinueWithOnMainThread((task) =>
			{
				if (!task.IsCompleted && task.Exception == null)
				{
					user.SetToken(task.Result);
				}

				internalUser?.SetToken(task.Result);
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
						RequestTokenInternal(internalFirebaseUser, OnSignedIn);
					}
					else
					{
						OnFailed?.Invoke();
					}
				});
			}
			else
			{
				var provider = credential.GetProvider();

				switch (provider)
				{
					case ProviderType.Anonymous:
					{
						auth.SignInAnonymouslyAsync().ContinueWithOnMainThread((task) =>
						{
							if (task.IsCompleted && task.Exception == null)
							{
								var user = FromFirebaseUser(task.Result);
								OnSignedIn?.Invoke(user);
							}
							else
							{
								OnFailed?.Invoke();
							}
						});
					}
					break;

					case ProviderType.Custom:
					{
						auth.SignInWithCustomTokenAsync(credential.GetToken()).ContinueWithOnMainThread((task) =>
						{
							if (task.IsCompleted && task.Exception == null)
							{
								var user = FromFirebaseUser(task.Result);
								OnSignedIn?.Invoke(user);
							}
							else
							{
								OnFailed?.Invoke();
							}
						});
					}
					break;

					default:
					{
						OnFailed?.Invoke();
					}
					break;
				}
			}
		}

		private void OnAuthStateChangedHandler(object sender, System.EventArgs e)
		{
			var auth = FirebaseAuth.DefaultInstance;

			if (auth.CurrentUser != internalFirebaseUser)
			{
				bool signedIn = internalFirebaseUser != auth.CurrentUser && auth.CurrentUser != null;
				
				if (!signedIn && internalFirebaseUser != null)
				{
					internalUser = null;
					internalFirebaseUser = null;
				}

				internalFirebaseUser = auth.CurrentUser;

				if (signedIn)
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
			
			return user;
		}

		private FirebaseCredential ToFirebaseCredential(Credential credential)
		{
			var provider = credential.GetProvider();

			switch (provider)
			{
				case ProviderType.EmailPassword:
				{
					var creds = EmailAuthProvider.GetCredential(credential.GetEmail(), credential.GetPassword());
					return creds;
				}
				break;

				case ProviderType.Facebook:
				{
					var creds = FacebookAuthProvider.GetCredential(credential.GetToken());
					return creds;
				}
				break;

				case ProviderType.Apple:
				{
					var creds = OAuthProvider.GetCredential("apple.com", credential.GetToken(), credential.GetNonce(), null);
					return creds;
				}
				break;
			}

			return null;
		}
	}
}
