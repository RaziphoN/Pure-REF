using System.Collections.Generic;

namespace REF.Runtime.Online.Store
{
	// this is a mock-up to implement on game side to process offline purchase, because it is not possible to decouple profile remove things and such from game code
	public interface IOfflineBillingMethod
	{
		void Buy(IProduct product, System.Action<ITransaction> OnSuccess, System.Action OnNotEnoughResources, System.Action<ITransaction> OnFailed);
	}

	public interface IStore : IOnlineService
	{
		string GetStoreId();

		void Buy(IProduct product, System.Action<ITransaction> OnSuccess, System.Action OnNotEnoughResources, System.Action<ITransaction> OnFailed);

		IProduct GetProductById(string id);
		IProduct GetProductByStoreId(string storeId);
		IProduct GetProductByIdx(string id);
		int GetProductCount();

		IEnumerable<IProduct> GetOfflineProducts(); // products that may be bought using in-game resources
		IEnumerable<IProduct> GetCurrencyProducts();
		IEnumerable<IProduct> GetProducts();
	}
}
