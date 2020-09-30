using UnityEngine;

using System.Linq;
using System.Collections.Generic;

namespace REF.Runtime.GameSystem.Storage
{
	public interface IItemContainer
	{
		bool HasItem(IItem item);
		bool HasItems(IItemContainer container);
		bool HasItemOfType(ItemType type);
		IItem GetItemOfType(ItemType type);
		IList<IItem> GetItemsOfType(ItemType type);
		IList<IItem> GetItems();

		int GetItemCountOfType(ItemType type);
		int GetTotalItemCount();

		void Stack();
		void AddItem(IItem item);
		void AddItemsFrom(ItemContainer container);
		void AddItemAndStack(IItem item);
		void Remove(IItem item);
		void Remove(IItemContainer container);
		void RemoveAllItemOfType(ItemType type);
		int GetItemCount();
		IItem GetItem(int idx);
		IItemContainer Clone();
	}

	public interface IItemContainer<T> : IItemContainer where T : IItem
	{
		bool HasItem(T item);
		bool HasItems(IItemContainer<T> container);
		bool HasItemOfType(ItemType type);
		T GetItemOfType(ItemType type);
		IList<T> GetItemsOfType(ItemType type);
		IList<T> GetItems();

		int GetItemCountOfType(ItemType type);
		int GetTotalItemCount();

		void Stack();
		void AddItem(T item);
		void AddItemsFrom(IItemContainer<T> container);
		void AddItemAndStack(T item);
		void Remove(T item);
		void Remove(IItemContainer<T> container);
		void RemoveAllItemOfType(ItemType type);
		int GetItemCount();
		T GetItem(int idx);
		IItemContainer<T> Clone();
	}

	[System.Serializable]
	public class ItemContainer : IItemContainer
	{
		[SerializeField] private IList<IItem> items = new List<IItem>();

		public bool HasItem(IItem item)
		{
			return items.HasItem(item);
		}

		public bool HasItems(IItemContainer container)
		{
			var content = container.GetItems();
			return content.All((item) => { return HasItem(item); });
		}

		public bool HasItemOfType(ItemType type)
		{
			return items.HasItemOfType(type);
		}

		public IItem GetItemOfType(ItemType type)
		{
			return items.GetItemOfType(type);
		}

		public IList<IItem> GetItemsOfType(ItemType type)
		{
			return items.GetItemsOfType(type);
		}

		public IList<IItem> GetItems()
		{
			return items;
		}

		public int GetItemCountOfType(ItemType type)
		{
			return items.GetItemCountOfType(type);
		}

		public int GetTotalItemCount()
		{
			return items.GetTotalItemCount();
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
			items.Add(item.Clone());
		}

		public void Remove(IItem item)
		{
			if (HasItem(item))
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

		public void RemoveAllItemOfType(ItemType type)
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

		public bool HasItem(T item)
		{
			return items.HasItem(item);
		}

		public bool HasItems(IItemContainer<T> container)
		{
			var content = container.GetItems();
			return content.All((item) => { return HasItem(item); });
		}

		public bool HasItemOfType(ItemType type)
		{
			return items.HasItemOfType(type);
		}

		public T GetItemOfType(ItemType type)
		{
			return items.GetItemOfType(type);
		}

		public IList<T> GetItemsOfType(ItemType type)
		{
			return items.GetItemsOfType(type);
		}

		public IList<T> GetItems()
		{
			return items;
		}

		public int GetItemCountOfType(ItemType type)
		{
			return items.GetItemCountOfType(type);
		}

		public int GetTotalItemCount()
		{
			return items.GetTotalItemCount();
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
			if (HasItem(item))
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

		public void RemoveAllItemOfType(ItemType type)
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

		IItem IItemContainer.GetItemOfType(ItemType type)
		{
			return items.GetItemOfType<T>(type);
		}

		IList<IItem> IItemContainer.GetItemsOfType(ItemType type)
		{
			return (IList<IItem>)items.GetItemsOfType<T>(type);
		}

		IList<IItem> IItemContainer.GetItems()
		{
			return (IList<IItem>)GetItems();
		}

		IItem IItemContainer.GetItem(int idx)
		{
			return GetItem(idx);
		}

		IItemContainer IItemContainer.Clone()
		{
			return Clone();
		}
	}
}
