#if REF_ONLINE_SOCIAL

using System.Collections.Generic;

namespace REF.Runtime.Online.Social
{
	public interface ISocialService : IOnlineService
	{
		bool IsSignedIn();

		string GetUserId();
		string GetToken();

		void LoadFriendList(System.Action<IList<ISocialUserProfile>> OnSuccess, System.Action OnFail = null);
		void LoadProfile(System.Action<ISocialUserProfile> OnSuccess, System.Action OnFail = null);

		void SignIn(System.Action OnSucess, System.Action OnFail);
		void SignOut();
	}
}

#endif
