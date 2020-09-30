using System.Linq;
using System.Collections.Generic;

namespace REF.Runtime.GameSystem.Storage
{
	public static class ItemExt
	{
		public static bool HasItem<T>(this IList<T> items, IItem item) where T : IItem
		{
			return items.Any((invItem) => { return invItem.IsSame(item) && invItem.GetQuantity() >= item.GetQuantity() && invItem.GetQuantity() > 0; });
		}

		public static T GetSameItemFromIList<T>(this IList<T> items, T source) where T : IItem
		{
			return items.FirstOrDefault((item) => { return item.IsSame(source); });
		}

		public static bool HasItemOfType<T>(this IList<T> items, ItemType type) where T : IItem
		{
			return items.Any((item) => { return item.GetItemType() == type; });
		}

		public static T GetItemOfType<T>(this IList<T> items, ItemType type) where T : IItem
		{
			return items.FirstOrDefault((item) => { return item.GetItemType() == type; });
		}

		public static IList<T> GetItemsOfType<T>(this IList<T> items, ItemType type) where T : IItem
		{
			return (IList<T>)items.Where((item) => { return item.GetItemType() == type; });
		}

		public static int GetItemCountOfType<T>(this IList<T> items, ItemType type) where T : IItem
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

		public static int GetTotalItemCount<T>(this IList<T> items) where T : IItem
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
}
