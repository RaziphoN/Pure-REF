#if REF_ONLINE_SOCIAL && REF_FACEBOOK_SOCIAL && REF_USE_FACEBOOK

using UnityEngine;

using System.Linq;
using System.Collections.Generic;

using REF.Runtime.Core;
using REF.Runtime.Diagnostic;
using REF.Runtime.Online.Service.Facebook;

using Facebook.Unity;

namespace REF.Runtime.Online.Social.Facebook
{
	public class FacebookSocialService : FacebookService, ISocialService
	{
		private int avatarWidth = 256;
		private int avatarHeight = 256;
		private string[] permissions;

		public override void Configure(IConfiguration config)
		{
			base.Configure(config);

			var configuration = config as ISocialServiceConfiguration;

			if (configuration == null)
			{
				RefDebug.Error(nameof(FacebookSocialService), $"Config must be of type {nameof(ISocialServiceConfiguration)}!");
				return;
			}

			permissions = configuration.GetPermissions();

			if (permissions == null)
			{
				permissions = new string[0];
			}

			avatarWidth = configuration.GetAvatarWidth();
			avatarHeight = configuration.GetAvatarHeight();
		}

		public string GetUserId()
		{
			if (!IsInitialized() || !IsSignedIn())
			{
				return string.Empty;
			}

			return AccessToken.CurrentAccessToken.UserId;
		}

		public string GetToken()
		{
			if (!IsInitialized() || !IsSignedIn())
			{
				return string.Empty;
			}

			return AccessToken.CurrentAccessToken.TokenString;
		}

		public bool IsSignedIn()
		{
			return FB.IsLoggedIn;
		}

		public void LoadFriendList(System.Action<IList<ISocialUserProfile>> OnSuccess, System.Action OnFail = null)
		{
			string query = $"/me/friends?fields=name,id,picture.width({avatarWidth}).height({avatarHeight})"; 

			FB.API(query, HttpMethod.GET, (graphResult) =>
			{
				if (graphResult.Cancelled || !string.IsNullOrEmpty(graphResult.Error))
				{
					OnFail?.Invoke();
					return;
				}

				var friends = JsonUtility.FromJson<Friends>(graphResult.RawResult);
				OnSuccess?.Invoke(friends.ToSocialProfiles());
			});
		}

		public void LoadProfile(System.Action<ISocialUserProfile> OnSuccess, System.Action OnFail = null)
		{
			string query = $"/me?fields=name,id,picture.width({avatarWidth}).height({avatarHeight})";

			FB.API(query, HttpMethod.GET, (graphResult) =>
			{
				if (graphResult.Cancelled || !string.IsNullOrEmpty(graphResult.Error))
				{
					OnFail?.Invoke();
					return;
				}

				var user = JsonUtility.FromJson<User>(graphResult.RawResult);
				OnSuccess?.Invoke(user.ToSocialProfile());
			});
		}

		public void SignIn(System.Action OnSuccess, System.Action OnFailed = null)
		{
			if (!IsInitialized())
			{
				OnFailed?.Invoke();
				return;
			}

			if (IsSignedIn())
			{
				OnSuccess?.Invoke();
				return;
			}

			FacebookDelegate<ILoginResult> logInCallback = (result) =>
			{
				if (IsSignedIn())
				{
					OnSuccess?.Invoke();
				}
				else
				{
					OnFailed?.Invoke();
				}
			};

			if (!HasPublishPermission())
			{
				FB.LogInWithReadPermissions(permissions, logInCallback);
			}
			else
			{
				FB.LogInWithPublishPermissions(permissions, logInCallback);
			}
		}

		public void SignOut()
		{
			if (!IsInitialized())
			{
				return;
			}

			if (!IsSignedIn())
			{
				return;
			}

			FB.LogOut();
		}

		public void Share(System.Uri url, string title = "", string description = "", System.Uri imageUrl = null, FacebookDelegate<IShareResult> callback = null)
		{
			FB.ShareLink(url, title, description, imageUrl, callback);
		}

		public void API(string query, HttpMethod method, FacebookDelegate<IGraphResult> callback, IDictionary<string, string> formData = null)
		{
			FB.API(query, method, callback, formData);
		}

		private bool HasPublishPermission()
		{
			return permissions.Any((permission) => { return permission.Contains("publish"); });
		}
	}
}

#endif