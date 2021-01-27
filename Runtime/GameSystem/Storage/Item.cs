namespace REF.Runtime.GameSystem.Storage
{
	public interface IItem
	{
		int GetMaxStackQuantity();

		void SetItemType(string type);
		string GetItemType();

		void SetQuantity(int quantity);
		int GetQuantity();

		bool IsStackable(IItem other); // returns true if item could stack with other item
		bool Stack(IItem other); // returns true if item is stacked, false if stack overflow (item quantity > max stack quantity)

		bool IsSame(IItem other); // identify items that are interexchangeable
		bool Equals(IItem other); // deep compare

		IItem Clone();
	}
}
