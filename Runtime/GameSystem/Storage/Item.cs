namespace REF.Runtime.GameSystem.Storage
{
	public enum ItemType
	{
		Count
	}

	public interface IItem
	{
		void SetItemType(ItemType type);
		ItemType GetItemType();

		void SetQuantity(int quantity);
		int GetQuantity();

		bool IsStackable(IItem other);
		bool Stack(IItem other);

		bool IsSame(IItem other);
		bool Equals(IItem other);

		IItem Clone();
	}
}
