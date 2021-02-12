using REF.Runtime.Core;

namespace REF.Runtime.Online.Store.Providers
{
	public interface IStoreProvider : IService
	{
		string GetProviderId();

		void Buy(IProduct product);
		void Restore();
	}

	public interface IStoreProvider<TConfig> : IStoreProvider, IOnlineService<TConfig> where TConfig : IConfiguration
	{
		
	}
}
