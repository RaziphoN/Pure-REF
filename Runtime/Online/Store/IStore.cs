using REF.Runtime.Core;

using System.Collections.Generic;

namespace REF.Runtime.Online.Store
{
	public interface IStore : IService
	{
		void Buy(string id, System.Action<ITransaction> OnSuccess, System.Action<ITransaction> OnFailed);
		void Buy(IProduct product, System.Action<ITransaction> OnSuccess, System.Action<ITransaction> OnFailed);
		void Buy(IProduct product, string providerId, System.Action<ITransaction> OnSuccess, System.Action<ITransaction> OnFailed);
		void Restore(System.Action<IList<IProduct>> OnSuccess, System.Action OnFailed);

		IProduct GetProductById(string id);
		IProduct GetProductByIdx(int idx);
		int GetProductCount();

		IEnumerable<IProduct> GetProducts();
	}
}
