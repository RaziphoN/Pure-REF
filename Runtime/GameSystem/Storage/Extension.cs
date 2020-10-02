using System.Linq;
using System.Collections.Generic;

namespace REF.Runtime.GameSystem.Storage
{
	public static class ItemExt
	{
		public static bool ContainsItem<T>(this IList<T> items, T item) where T : IItem
		{
			return items.Any((invItem) => { return invItem.IsSame(item) && invItem.GetQuantity() >= item.GetQuantity() && invItem.GetQuantity() > 0; });
		}

		public static T GetSameItemFromIList<T>(this IList<T> items, T source) where T : IItem
		{
			return items.FirstOrDefault((item) => { return item.IsSame(source); });
		}

		public static bool ContainsItemOfType<T>(this IList<T> items, string type) where T : IItem
		{
			return items.Any((item) => { return item.GetItemType() == type; });
		}

		public static T GetItemOfType<T>(this IList<T> items, string type) where T : IItem
		{
			return items.FirstOrDefault((item) => { return item.GetItemType() == type; });
		}

		public static IList<T> GetItemsOfType<T>(this IList<T> items, string type) where T : IItem
		{
			return items.Where((item) => { return item.GetItemType() == type; }).ToList();
		}

		public static int GetItemQuantityOfType<T>(this IList<T> items, string type) where T : IItem
		{
			int count = 0;

			for (int i = 0; i < items.Count; ++i)
			{
				var item = items[i];

				if (item.GetItemType() == type)
				{
					count += item.GetQuantity();
				}
			}

			return count;
		}

		public static int GetTotalItemQuantity<T>(this IList<T> items) where T : IItem
		{
			int count = 0;

			for (int i = 0; i < items.Count; ++i)
			{
				var item = items[i];
				count += item.GetQuantity();
			}

			return count;
		}
	}

	public static class ItemContainerExt
	{
		public static bool ContainsItem<T, U>(this IList<U> items, T item) where T : IItem where U : IItemContainer<T>
		{
			return items.Any((container) => { return container.ContainsItem(item); });
		}

		public static bool ContainsItemOfType<T, U>(this IList<U> items, string type) where T : IItem where U : IItemContainer<T>
		{
			return items.Any((container) => { return container.ContainsItemOfType(type); });
		}

		public static int GetItemQuantityOfType<T, U>(this IList<U> items, string type) where T : IItem where U : IItemContainer<T>
		{
			int quantity = 0;

			foreach (var container in items)
			{
				quantity += container.GetItemQuantityOfType(type);
			}

			return quantity;
		}

		public static int GetTotalItemQuantity<T, U>(this IList<U> items) where T : IItem where U : IItemContainer<T>
		{
			int quantity = 0;

			foreach (var container in items)
			{
				quantity += container.GetTotalItemQuantity();
			}

			return quantity;
		}
	}
}
