namespace REF.Runtime.GameSystem.Storage
{
	public interface IItem
	{
		void SetItemType(string type);
		string GetItemType();

		void SetQuantity(int quantity);
		int GetQuantity();

		bool IsStackable(IItem other);
		bool Stack(IItem other);

		bool IsSame(IItem other);
		bool Equals(IItem other);

		IItem Clone();
	}
}
