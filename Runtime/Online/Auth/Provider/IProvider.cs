namespace REF.Runtime.Online.Auth.Provider
{
	public interface IProvider
	{
		ProviderType GetProviderType();
		Credential ToCredential();
	}
}
