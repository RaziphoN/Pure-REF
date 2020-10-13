using System.Collections.Generic;

using REF.Runtime.GameSystem.Storage;

namespace REF.Runtime.Online.Store
{
	public interface IProduct
	{
		bool IsValid();

		ProductType GetProductType();
		string GetId(); // in-game identifier
		IItemContainer<T> GetContent<T>() where T : IItem;

		// default product info, read as MAIN
		bool IsEnabled();
		string GetProviderId();
		IProductMeta GetMeta();
		IPrice GetPrice();

		bool IsDefined(string providerId);
		bool IsEnabled(string providerId);
		IProductMeta GetMeta(string providerId);
		IPrice GetPrice(string providerId); // store unique identifier
	}
}
