#if REF_ONLINE_SOCIAL

using System.Collections.Generic;

using REF.Runtime.Core;

namespace REF.Runtime.Online.Social
{
	public interface ISocialService : IOnlineService<IConfiguration>
	{
		string GetUserId();
		string GetToken();
		bool IsSignedIn();

		void LoadFriendList(System.Action<IList<ISocialUserProfile>> OnSuccess, System.Action OnFail = null);
		void LoadProfile(System.Action<ISocialUserProfile> OnSuccess, System.Action OnFail = null);

		void SignIn(System.Action OnSucess, System.Action OnFail);
		void SignOut();
	}
}

#endif
