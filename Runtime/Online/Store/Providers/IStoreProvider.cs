#if REF_STORE

using REF.Runtime.Core;

namespace REF.Runtime.Online.Store.Providers
{
	public interface IStoreProvider : IOnlineService
	{
		string GetProviderId();

		void Buy(IProduct product);
		void Restore();
	}
}

#endif
