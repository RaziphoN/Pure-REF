
namespace REF.Runtime.Online.Store
{
	public interface IProductMeta
	{
		bool IsBestDeal();

		bool IsDiscount();
		int GetDiscountPercentage();
		System.DateTime GetDiscountEndDate();

		string GetLocalizedTitle();
		string GetLocalizedDescription();
	}
}
