using REF.Runtime.GameSystem.Storage;

namespace REF.Runtime.Online.Store
{
	public enum PriceType
	{
		Item = 0,
		Currency = 1,
	}

	public interface IPrice
	{
		PriceType GetPriceType();
	}

	public interface IItemPrice : IPrice
	{
		IItemContainer<T> GetPrice<T>() where T : IItem;
	}

	public interface ICurrencyPrice : IPrice
	{
		string GetPrice();
		string GetLocalizedPrice();
		string GetCurrencyCode();
	}
}
