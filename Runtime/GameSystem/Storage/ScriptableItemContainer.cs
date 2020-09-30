using UnityEngine;

using System.Collections.Generic;

namespace REF.Runtime.GameSystem.Storage
{
	[CreateAssetMenu(fileName = "ItemContainer", menuName = "Scriptable/Game/Items/Item Container")]
	public class ScriptableItemContainer : ScriptableObject, IItemContainer
	{
		[SerializeField] private ItemContainer container = new ItemContainer();

		public void AddItem(Item item)
		{
			container.AddItem(item);
		}

		public void AddItemAndStack(Item item)
		{
			container.AddItemAndStack(item);
		}

		public void AddItemsFrom(ItemContainer container)
		{
			this.container.AddItemsFrom(container);
		}

		public IItemContainer Clone()
		{
			return container.Clone();
		}

		public Item GetItem(int idx)
		{
			return container.GetItem(idx);
		}

		public int GetItemCount()
		{
			return container.GetItemCount();
		}

		public int GetItemCountOfType(ItemType type)
		{
			return container.GetItemCountOfType(type);
		}

		public Item GetItemOfType(ItemType type)
		{
			return container.GetItemOfType(type);
		}

		public List<Item> GetItems()
		{
			return container.GetItems();
		}

		public List<Item> GetItemsOfType(ItemType type)
		{
			return container.GetItemsOfType(type);
		}

		public int GetTotalItemCount()
		{
			return container.GetTotalItemCount();
		}

		public bool HasItem(Item item)
		{
			return container.HasItem(item);
		}

		public bool HasItemOfType(ItemType type)
		{
			return container.HasItemOfType(type);
		}

		public bool HasItems(IItemContainer container)
		{
			return this.container.HasItems(container);
		}

		public void Remove(Item item)
		{
			container.Remove(item);
		}

		public void Remove(IItemContainer container)
		{
			this.container.Remove(container);
		}

		public void RemoveAllItemOfType(ItemType type)
		{
			container.RemoveAllItemOfType(type);
		}

		public void Stack()
		{
			container.Stack();
		}
	}
}
