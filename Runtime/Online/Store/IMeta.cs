#if REF_STORE

namespace REF.Runtime.Online.Store
{
	public interface IMeta
	{
		bool IsBestDeal();
		void SetBestDeal(bool state);

		bool IsDiscount();
		void SetDiscount(bool state, int percentage, System.DateTime endDate);

		int GetDiscountPercentage();
		System.DateTime GetDiscountEndDate();

		string GetLocalizedTitle();
		void SetLocalizedTitle(string title);

		string GetLocalizedDescription();
		void SetLocalizedDescription(string description);
	}
}

#endif
