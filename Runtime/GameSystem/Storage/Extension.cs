using System.Linq;
using System.Collections.Generic;

using REF.Runtime.Utilities.Extension;

namespace REF.Runtime.GameSystem.Storage
{
	public static class ItemExt
	{
		private class ItemCount
		{
			public int StackQuantity;
			public int Remainder;

			public static bool operator>(ItemCount lhs, ItemCount rhs)
			{
				if (lhs.StackQuantity > rhs.StackQuantity)
				{
					return true;
				}

				if (lhs.Remainder > rhs.Remainder && lhs.StackQuantity == rhs.StackQuantity)
				{
					return true;
				}

				return false;
			}

			public static bool operator<(ItemCount lhs, ItemCount rhs)
			{
				if (lhs.StackQuantity < rhs.StackQuantity)
				{
					return true;
				}

				if (lhs.Remainder < rhs.Remainder && lhs.StackQuantity == rhs.StackQuantity)
				{
					return true;
				}

				return false;
			}
		}

		public static bool ContainsItem<T>(this IEnumerable<T> items, T item) where T : IItem
		{
			return items.Any((invItem) => { return invItem.IsSame(item) && invItem.GetQuantity() >= item.GetQuantity() && invItem.GetQuantity() > 0; });
		}

		public static bool ContainItems<T>(this IEnumerable<T> items, IEnumerable<T> cost) where T : IItem
		{
			var enumerator = cost.GetEnumerator();

			while (enumerator.MoveNext())
			{
				var item = enumerator.Current;

				var stackSize = item.GetMaxStackQuantity();
				var sameCostItems = cost.Where((i) => { return i.IsSame(item); });
				var sameItems = items.Where((i) => { return i.IsSame(item); });

				var count = sameItems.GetItemCountOfSameItems(stackSize);
				var costCount = sameCostItems.GetItemCountOfSameItems(stackSize);

				if (count < costCount)
				{
					return false;
				}
			}

			return true;
		}

		public static T GetSameItemFromList<T>(this IEnumerable<T> items, T source) where T : IItem
		{
			return items.FirstOrDefault((item) => { return item.IsSame(source); });
		}

		public static bool ContainsItemOfType<T>(this IEnumerable<T> items, string type) where T : IItem
		{
			return items.Any((item) => { return item.GetItemType() == type; });
		}

		public static T GetItemOfType<T>(this IEnumerable<T> items, string type) where T : IItem
		{
			return items.FirstOrDefault((item) => { return item.GetItemType() == type; });
		}

		public static IList<T> GetItemsOfType<T>(this IEnumerable<T> items, string type) where T : IItem
		{
			return items.Where((item) => { return item.GetItemType() == type; }).ToList();
		}

		public static int GetItemQuantityOfType<T>(this IEnumerable<T> items, string type) where T : IItem
		{
			int count = 0;

			foreach (var item in items)
			{
				if (item.GetItemType() == type)
				{
					count = count.SafeAdd(item.GetQuantity());
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
				count = count.SafeAdd(item.GetQuantity());
			}

			return count;
		}

		private static ItemCount GetItemCountOfSameItems<T>(this IEnumerable<T> items, int stackSize) where T : IItem
		{
			ItemCount count = new ItemCount();

			foreach (var item in items)
			{
				var quantity = item.GetQuantity();

				if (quantity == stackSize)
				{
					count.StackQuantity++;
				}
				else
				{
					var diff = stackSize - quantity;
					if (count.Remainder > diff)
					{
						count.StackQuantity++;
						count.Remainder -= diff;
					}
					else
					{
						count.Remainder += quantity;
					}
				}
			}

			return count;
		}
	}

	public static class ItemContainerExt
	{
		public static bool ContainsItem<T, U>(this IList<U> items, T item) where T : IItem where U : IItemContainer<T>
		{
			return items.Any((container) => { return container.Contains(item); });
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
				quantity = quantity.SafeAdd(container.GetItemQuantityOfType(type));
			}

			return quantity;
		}

		public static int GetTotalItemQuantity<T, U>(this IList<U> items) where T : IItem where U : IItemContainer<T>
		{
			int quantity = 0;

			foreach (var container in items)
			{
				quantity = quantity.SafeAdd(container.GetTotalItemQuantity());
			}

			return quantity;
		}
	}
}
