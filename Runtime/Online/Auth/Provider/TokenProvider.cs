#if REF_ONLINE_AUTH

namespace REF.Runtime.Online.Auth.Provider
{
	public abstract class TokenProvider : IProvider
	{
		private string token;

		public abstract ProviderType GetProviderType();

		public Credential ToCredential()
		{
			Credential credential = new Credential();
			credential.SetUpToken(GetProviderType(), token);

			return credential;
		}

		protected void SetToken(string token)
		{
			this.token = token;
		}
	}
}

#endif
