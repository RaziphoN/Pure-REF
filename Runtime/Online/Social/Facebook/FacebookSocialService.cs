using UnityEngine;

using System.Linq;
using System.Collections.Generic;

using REF.Runtime.Online.Service.Facebook;

using Facebook.Unity;

namespace REF.Runtime.Online.Social.Facebook
{
	[CreateAssetMenu(fileName = "FacebookSocialService", menuName = "REF/Online/Social/Facebook")]
	public class FacebookSocialService : FacebookService, ISocialService
	{
		[SerializeField] private Vector2Int avatarResolution = new Vector2Int(50, 50);
		[SerializeField] private List<string> logInPermissions = new List<string>();

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
			string query = $"/me/friends?fields=name,id,picture.width({avatarResolution.x}).height({avatarResolution.y})"; 

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
			string query = $"/me?fields=name,id,picture.width({avatarResolution.x}).height({avatarResolution.y})";

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
				FB.LogInWithReadPermissions(logInPermissions, logInCallback);
			}
			else
			{
				FB.LogInWithPublishPermissions(logInPermissions, logInCallback);
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
			return logInPermissions.Any((permission) => { return permission.Contains("publish"); });
		}
	}
}
