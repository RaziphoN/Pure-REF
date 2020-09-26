#if REF_ONLINE_SOCIAL && REF_FACEBOOK_SOCIAL && REF_USE_FACEBOOK
using REF.Runtime.Online.Social.Facebook;

namespace REF.Runtime.Online.Auth.Method
{
	public class FacebookSignInMethod : ISignInMethod
	{
		public static string ProviderId { get { return "facebook.com"; } }

		private FacebookSocialService fb;

		public FacebookSignInMethod(FacebookSocialService facebookService)
		{
			fb = facebookService;
		}

		public string GetProviderId()
		{
			return ProviderId;
		}

		public void SignIn(System.Action<Credential> OnSuccess, System.Action OnFail)
		{
			fb.SignIn(() =>
			{
				var credential = new Credential();
				credential.SetProviderId(ProviderId);
				credential.SetToken(fb.GetToken());
				OnSuccess?.Invoke(credential);

			}, OnFail);
		}

		public void SignOut()
		{
			fb.SignOut();
		}
	}
}

#endif
