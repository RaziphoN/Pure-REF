using System;
using System.Linq;
using System.Collections.Generic;

namespace REF.Runtime.Utilities.Extension
{
    public static class ListExtension
    {
        public static T Random<T>(this T[] array, Random rnd)
        {
            if (array == null || array.Length == 0)
            {
                return default(T);
            }
            return array[rnd.Next(0, array.Length)];
        }

        public static void RemoveRange<T>(this IList<T> list, IList<T> removeList)
        {
            foreach (T obj in removeList)
            {
                if (list.Contains(obj))
                {
                    list.Remove(obj);
                }
            }
        }
        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        public static void RemoveAll<T>(this IList<T> list, T toRemove)
        {
            while (list.Remove(toRemove))
            {
            }
        }

        public static T Random<T>(this IList<T> list, Random rnd)
        {
            if (list == null || list.Count == 0)
            {
                return default(T);
            }
            return list[rnd.Next(0, list.Count)];
        }

        public static T Random<T>(this IList<T> list)
        {
			Random rnd = new Random();
            return list.Random(rnd);
        }

        public static void Shuffle<T>(this T[] list)
        {
            Random rng = new Random();
            int n = list.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this IList<T> list, Random rnd)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

		public static void Shuffle<T>(this IList<T> list)
		{
			Random rnd = new Random();
			list.Shuffle(rnd);
		}

		public static void Shuffle<T>(this IList<T> list, int seed)
		{
			Random rnd = new Random(seed);
			list.Shuffle(rnd);
		}

		public static void SetLength<T>(this List<T> list, int length)
        {
            while (list.Count > length)
            {
                list.RemoveAt(list.Count - 1);
            }
            while (list.Count < length)
            {
                list.Add(default(T));
            }
        }

        public static void Resort<T>(this IList<T> list)
        {
                var firstElem = list[0];
                list.RemoveAt(0);
                list.Shuffle();
                list.Add(firstElem);
        }

        public static void Scroll<T>(this List<T> list, int offset)
        {
            offset = offset % list.Count;
            if (offset < 0)
            {
                offset = list.Count + offset;
            }
            List<T> copy = new List<T>(list);
            for (int i = 0; i < list.Count; i++)
            {
                int index = (list.Count - offset + i) % list.Count;
                list[i] = copy[index];
            }
        }

        public static void Scroll<T>(this T[] array, int offset)
        {
            offset = offset % array.Length;
            if (offset < 0)
            {
                offset = array.Length + offset;
            }
            T[] copy = new T[array.Length];
            Array.Copy(array, copy, array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                int index = (array.Length - offset + i) % array.Length;
                array[i] = copy[index];
            }
        }

        public static int FindIndex<T>(this T[] array, Func<T, bool> func)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (func(array[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static void Add<T>(this IList<T> list, T obj, int count)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(obj);
            }
        }

        

    }
}
