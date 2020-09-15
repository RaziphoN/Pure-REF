#if REF_ONLINE_AUTH

namespace REF.Runtime.Online.Auth.Provider
{
	public class FacebookProvider : TokenProvider
	{
		public static string ProviderId { get { return "Facebook"; } }

		public override ProviderType GetProviderType()
		{
			return ProviderType.Facebook;
		}
	}
}

#endif
