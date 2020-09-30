using UnityEngine;

using System.Linq;
using System.Collections.Generic;

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
		void AddItemsFrom(IItemContainer<T> container);
		void AddItemAndStack(T item);

		void Remove(T item);
		void Remove(IItemContainer<T> container);
		void RemoveAllItemOfType(string type);

		IItemContainer<T> Clone();
	}

	[System.Serializable]
	public class ItemContainer : IItemContainer
	{
		[SerializeField] private IList<IItem> items = new List<IItem>();

		public bool Contains(IItem item)
		{
			return items.HasItem(item);
		}

		public bool Contains(IItemContainer container)
		{
			var content = container.GetItems();
			return content.All((item) => { return Contains(item); });
		}

		public bool ContainsItemOfType(string type)
		{
			return items.HasItemOfType(type);
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
				AddItem(container.GetItem(i).Clone());
			}
		}

		public void AddItem(IItem item)
		{
			items.Add(item.Clone());
		}

		public void Remove(IItem item)
		{
			if (Contains(item))
			{
				var invItem = items.GetSameItemFromIList<IItem>(item);

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

				if (stacked.Count > 0)
				{
					for (int j = 0; j < stacked.Count; ++j)
					{
						var stackedItem = stacked[j];

						if (stackedItem.IsStackable(item))
						{
							stackedItem.Stack(item);
							break;
						}

						if (j == stacked.Count - 1)
						{
							stacked.Add(item);
							break;
						}
					}
				}
				else
				{
					stacked.Add(item);
				}
			}

			items.Clear();
			items = stacked;
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
		[SerializeField] private IList<T> items = new List<T>();

		public bool Contains(T item)
		{
			return items.HasItem(item);
		}

		public bool Contains(IItemContainer<T> container)
		{
			var content = container.GetItems();
			return content.All((item) => { return Contains(item); });
		}

		public bool ContainsItemOfType(string type)
		{
			return items.HasItemOfType(type);
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

		public void AddItemsFrom(IItemContainer<T> container)
		{
			for (int i = 0; i < container.GetItemCount(); ++i)
			{
				AddItem((T)container.GetItem(i).Clone());
			}
		}

		public void AddItemAndStack(T item)
		{
			var existItem = items.FirstOrDefault((rewardItem) => { return rewardItem.GetItemType() == item.GetItemType(); });

			if (existItem != null)
			{
				int count = existItem.GetQuantity();
				count += item.GetQuantity();
				existItem.SetQuantity(count);
			}
			else
			{
				AddItem((T)item.Clone());
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
				var invItem = items.GetSameItemFromIList<T>(item);

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

				if (stacked.Count > 0)
				{
					for (int j = 0; j < stacked.Count; ++j)
					{
						var stackedItem = stacked[j];

						if (stackedItem.IsStackable(item))
						{
							stackedItem.Stack(item);
							break;
						}

						if (j == stacked.Count - 1)
						{
							stacked.Add(item);
							break;
						}
					}
				}
				else
				{
					stacked.Add(item);
				}
			}

			items.Clear();
			items = stacked;
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

		public bool HasItem(IItem item)
		{
			return items.HasItem(item);
		}

		public bool HasItems(IItemContainer container)
		{
			var content = container.GetItems();
			return content.All((item) => { return HasItem(item); });
		}

		public void AddItemsFrom(ItemContainer container)
		{
			for (int i = 0; i < container.GetItemCount(); ++i)
			{
				AddItem(container.GetItem(i).Clone());
			}
		}

		public void AddItemAndStack(IItem item)
		{
			var existItem = items.FirstOrDefault((rewardItem) => { return rewardItem.GetItemType() == item.GetItemType(); });

			if (existItem != null)
			{
				int count = existItem.GetQuantity();
				count += item.GetQuantity();
				existItem.SetQuantity(count);
			}
			else
			{
				AddItem(item.Clone());
			}
		}

		public void AddItem(IItem item)
		{
			items.Add((T)item.Clone());
		}

		public void Remove(IItem item)
		{
			if (HasItem(item))
			{
				var invItem = items.GetSameItemFromIList<T>((T)item);

				item.SetQuantity(item.GetQuantity() * -1);
				invItem.Stack(item);
				item.SetQuantity(item.GetQuantity() * -1); // return source item back in it's state

				if (invItem.GetQuantity() == 0)
				{
					items.Remove((T)invItem);
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
	}
}
