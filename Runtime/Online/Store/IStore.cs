using System.Collections.Generic;

using REF.Runtime.Core;

namespace REF.Runtime.Online.Store
{
	public interface IStore : IStoreListener, IOnlineService<IConfiguration>
	{
		void Buy(string id, System.Action<ITransaction> OnSuccess, System.Action<ITransaction> OnFailed);
		void Buy(IProduct product, System.Action<ITransaction> OnSuccess, System.Action<ITransaction> OnFailed);
		void Buy(IProduct product, string providerId, System.Action<ITransaction> OnSuccess, System.Action<ITransaction> OnFailed);

		void Restore();

		IProduct GetProductById(string id);
		IProduct GetProductByStoreId(string providerId, string id);
		IProduct GetProductByIdx(int idx);
		int GetProductCount();

		IEnumerable<IProduct> GetProducts();
	}
}
