#if REF_ONLINE_AUTH

using REF.Runtime.Core;

namespace REF.Runtime.Online.Auth
{
	public interface IAuthService : IOnlineService<IConfiguration>
	{
		event System.Action OnTokenChanged;

		bool IsSignedIn();
		User GetUser();

		void ChangeUserInfo(UserUpdate update, System.Action OnSuccess = null, System.Action OnFail = null);

		void Link(Credential credential, System.Action<User> OnLinked = null, System.Action OnFailed = null);

		void SignUp(Credential credential, System.Action<User> OnSignedIn = null, System.Action OnFailed = null);
		void SignIn(Credential credential, System.Action<User> OnSignedIn = null, System.Action OnFailed = null);
		void ReAuth(Credential credential, System.Action OnSucess = null, System.Action OnFailed = null);
		void SignOut();
	}
}

#endif