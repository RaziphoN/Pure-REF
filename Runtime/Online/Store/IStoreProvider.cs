using System.Collections.Generic;

namespace REF.Runtime.Online.Store
{
	public interface IStoreProvider : IOnlineService
	{
		// TODO: Add initialization with callback

		string GetProviderId();
		void Buy(IProduct product, System.Action<ITransaction> OnSuccess, System.Action<ITransaction> OnFailed);
		void Restore(System.Action<IList<IProduct>> OnSuccess, System.Action OnFailed);
	}
}
