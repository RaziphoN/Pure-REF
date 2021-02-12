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
		bool IsAvailable();

		string GetProviderId();
		IProductMeta GetMeta();
		IPrice GetPrice();

		bool IsDefined(string providerId);
		bool IsEnabled(string providerId);
		bool IsAvailable(string providerId);

		void SetEnabled(string providerId, bool state);
		void SetAvailable(string provderId, bool state);

		IEnumerable<KeyValuePair<string, string>> GetStoreIds(string providerId);
		IProductMeta GetMeta(string providerId);
		IPrice GetPrice(string providerId); // store unique identifier
	}
}
