using System.Collections.Generic;

using REF.Runtime.GameSystem.Storage;

namespace REF.Runtime.Online.Store
{
	public interface IProduct
	{
		bool IsAvailable();
		bool IsEnabled();

		string GetId(); // in-game identifier
		string GetStoreId(string store); // real store identifier or id of a product in 3rd party SDK that is used as store accessor, store is an unique identifier of a store

		IProductMeta GetMeta();
		IItemContainer GetContent();
		IItemContainer<T> GetContent<T>() where T : IItem;

		IPrice GetPriceByType(PriceType type);
		IPrice GetPrice();
		IList<IPrice> GetPrices();
	}
}
