using UnityEngine;

using System.Collections.Generic;

using REF.Runtime.Diagnostic;

namespace REF.Runtime.GameSystem.Storage
{
	public interface IItemContainer
	{
		bool Contains(IItem item);
		bool Contains(IItemContainer container);
		bool ContainsItemOfType(string type);
		
		IItem GetItemOfType(string type);
		IList<IItem> GetItemsOfType(string type);
		IList<IItem> GetItems();
		IItem GetItem(int idx);

		int GetItemQuantityOfType(string type);
		int GetTotalItemQuantity();
		int GetItemCount();

		void Stack();
		void AddItem(IItem item);
		void AddItems(IItemContainer container);
		
		void Remove(IItem item);
		void Remove(IItemContainer container);
		void RemoveAllItemOfType(string type);

		IItemContainer Clone();
		void Copy(IItemContainer container);
	}

	public interface IItemContainer<T> where T : IItem
	{
		bool Contains(T item);
		bool Contains(IItemContainer<T> container);
		bool ContainsItemOfType(string type);
		
		T GetItemOfType(string type);
		T GetItem(int idx);
		IList<T> GetItemsOfType(string type);
		IList<T> GetItems();

		int GetItemQuantityOfType(string type);
		int GetTotalItemQuantity();
		int GetItemCount();

		void Stack();
		void AddItem(T item);
		void AddItems(IItemContainer<T> container);

		void Remove(T item);
		void Remove(IItemContainer<T> container);
		void RemoveAllItemsOfType(string type);

		IItemContainer<T> Clone();
		void Copy(IItemContainer<T> container);
	}

	[System.Serializable]
	public class ItemContainer : IItemContainer
	{
		[SerializeField] protected List<IItem> items = new List<IItem>();

		public bool Contains(IItem item)
		{
			return items.Contains(item);
		}

		public bool Contains(IItemContainer container)
		{
			var content = container.GetItems();
			return items.ContainItems(content);
		}

		public bool ContainsItemOfType(string type)
		{
			return items.ContainsItemOfType(type);
		}

		public IItem GetItemOfType(string type)
		{
			return items.GetItemOfType(type);
		}

		public IList<IItem> GetItemsOfType(string type)
		{
			return items.GetItemsOfType(type);
		}

		public IList<IItem> GetItems()
		{
			return items;
		}

		public int GetItemQuantityOfType(string type)
		{
			return items.GetItemQuantityOfType(type);
		}

		public int GetTotalItemQuantity()
		{
			return items.GetTotalItemQuantity();
		}

		public void AddItems(IItemContainer container)
		{
			for (int i = 0; i < container.GetItemCount(); ++i)
			{
				AddItem(container.GetItem(i).Clone()); // Clone is questionable, but here i think it's more ok than not ok
			}
		}

		public void AddItem(IItem item)
		{
			items.Add(item.Clone()); // Clone is questionable
		}

		public void Remove(IItem item)
		{
			if (Contains(item))
			{
				var invItem = items.GetSameItemFromList<IItem>(item);

				item.SetQuantity(item.GetQuantity() * -1);
				invItem.Stack(item);
				item.SetQuantity(item.GetQuantity() * -1); // return source item back in it's state

				if (invItem.GetQuantity() == 0)
				{
					items.Remove(invItem);
				}
			}
		}

		public void Remove(IItemContainer container)
		{
			for (int idx = 0; idx < container.GetItemCount(); ++idx)
			{
				var item = container.GetItem(idx);
				Remove(item);
			}
		}

		public void RemoveAllItemOfType(string type)
		{
			for (int i = GetItemCount() - 1; i >= 0; --i)
			{
				var item = GetItem(i);
				if (item.GetItemType() == type)
				{
					items.Remove(item);
				}
			}
		}

		public int GetItemCount()
		{
			return items.Count;
		}

		public IItem GetItem(int idx)
		{
			return items[idx];
		}

		public void Stack()
		{
			List<IItem> stacked = new List<IItem>();

			for (int i = 0; i < items.Count; ++i)
			{
				var item = items[i];

				for (int j = 0; j < stacked.Count; ++j)
				{
					var stackedItem = stacked[j];

					if (stackedItem.IsStackable(item))
					{
						var stackSize = stackedItem.GetMaxStackQuantity();
						var quantity = stackedItem.GetQuantity();
						var itemToStackQuantity = item.GetQuantity();
						var stackQuantity = stackSize - quantity;

						if (stackQuantity > 0)
						{
							item.SetQuantity(stackQuantity);
							stackedItem.Stack(item);
							item.SetQuantity(itemToStackQuantity - stackQuantity);
							continue;
						}

						if (item.GetQuantity() == 0)
						{
							break;
						}
					}
				}

				var itemQuantity = item.GetQuantity();
				if (itemQuantity > 0)
				{
					stacked.Add(item);
				}
			}

			items.Clear();
			items = stacked;
		}

		public virtual void Copy(IItemContainer container)
		{
			items.Clear();

			for (int idx = 0; idx < container.GetItemCount(); ++idx)
			{
				var item = container.GetItem(idx);
				AddItem(item.Clone());
			}
		}

		public virtual IItemContainer Clone()
		{
			var container = new ItemContainer();
			container.items = new List<IItem>(items.Count);
			for (int i = 0; i < items.Count; ++i)
			{
				container.items.Add(items[i].Clone());
			}

			return container;
		}
	}

	[System.Serializable]
	public class ItemContainer<T> : IItemContainer<T> where T : IItem
	{
		[SerializeField] protected List<T> items = new List<T>();

		public bool Contains(T item)
		{
			return items.ContainsItem(item);
		}

		public bool Contains(IItemContainer<T> container)
		{
			var content = container.GetItems();
			return items.ContainItems(content);
		}

		public bool ContainsItemOfType(string type)
		{
			return items.ContainsItemOfType(type);
		}

		public T GetItemOfType(string type)
		{
			return items.GetItemOfType(type);
		}

		public IList<T> GetItemsOfType(string type)
		{
			return items.GetItemsOfType(type);
		}

		public IList<T> GetItems()
		{
			return items;
		}

		public int GetItemQuantityOfType(string type)
		{
			return items.GetItemQuantityOfType(type);
		}

		public int GetTotalItemQuantity()
		{
			return items.GetTotalItemQuantity();
		}

		public void AddItems(IItemContainer<T> container)
		{
			var count = container.GetItemCount();

			for (int i = 0; i < count; ++i)
			{
				AddItem((T)container.GetItem(i).Clone());
			}
		}

		public void AddItem(T item)
		{
			items.Add((T)item.Clone());
		}

		public void Remove(T item)
		{
			if (Contains(item))
			{
				var invItem = items.GetSameItemFromList<T>(item);

				item.SetQuantity(item.GetQuantity() * -1);
				invItem.Stack(item);
				item.SetQuantity(item.GetQuantity() * -1); // return source item back in it's state

				if (invItem.GetQuantity() == 0)
				{
					items.Remove(invItem);
				}
			}
		}

		public void Remove(IItemContainer<T> container)
		{
			for (int idx = 0; idx < container.GetItemCount(); ++idx)
			{
				var item = container.GetItem(idx);
				Remove(item);
			}
		}

		public void RemoveAllItemsOfType(string type)
		{
			for (int i = GetItemCount() - 1; i >= 0; --i)
			{
				var item = GetItem(i);
				if (item.GetItemType() == type)
				{
					items.Remove(item);
				}
			}
		}

		public int GetItemCount()
		{
			return items.Count;
		}

		public T GetItem(int idx)
		{
			return items[idx];
		}

		public void Stack()
		{
			List<T> stacked = new List<T>();

			for (int i = 0; i < items.Count; ++i)
			{
				var item = items[i];

				for (int j = 0; j < stacked.Count; ++j)
				{
					var stackedItem = stacked[j];

					if (stackedItem.IsStackable(item))
					{
						var stackSize = stackedItem.GetMaxStackQuantity();
						var quantity = stackedItem.GetQuantity();
						var itemToStackQuantity = item.GetQuantity();
						var stackAvailableQuantity = stackSize - quantity;

						if (stackAvailableQuantity <= itemToStackQuantity)
						{
							item.SetQuantity(stackAvailableQuantity);
							stackedItem.Stack(item);
							item.SetQuantity(itemToStackQuantity - stackAvailableQuantity);
							continue;
						}
						else
						{
							stackedItem.Stack(item);
							item.SetQuantity(0);
						}

						if (item.GetQuantity() == 0)
						{
							break;
						}
					}
				}

				var itemQuantity = item.GetQuantity();
				if (itemQuantity > 0)
				{
					stacked.Add(item);
				}
			}

			items.Clear();
			items = stacked;
		}

		public virtual void Copy(IItemContainer<T> container)
		{
			var containerItems = container.GetItems();
			Copy(containerItems);
		}

		public virtual void Copy(IEnumerable<T> collection)
		{
			if (collection == items)
			{
				return;
			}

			items.Clear();

			foreach (var item in collection)
			{
				AddItem((T)item.Clone());
			}
		}

		public virtual IItemContainer<T> Clone()
		{
			var container = new ItemContainer<T>();
			container.items = new List<T>(items.Count);
			for (int i = 0; i < items.Count; ++i)
			{
				container.items.Add((T)items[i].Clone());
			}

			return container;
		}
	}
}
