﻿#if REF_STORE

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
		decimal GetLocalizedPrice();
		void SetLocalizedPrice(decimal price);

		string GetLocalizedPriceString();
		void SetLocalizedPriceString(string priceString);

		string GetCurrencyCode();
		void SetCurrencyCode(string isoCountryCode);
	}
}

#endif