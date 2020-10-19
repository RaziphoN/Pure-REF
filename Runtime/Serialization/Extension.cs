
using System.Linq;
using System.Text;
using System.Collections.Generic;

using REF.Runtime.Preference;

namespace REF.Runtime.Serialization
{
	public static class Extension
	{
		[System.Serializable]
		public class SerializableKeyValuePair
		{
			public string Key;
			public string Value;
		}

		[System.Serializable]
		private class SerializableDictionaryList
		{
			public List<SerializableKeyValuePair> Data = null;

			public SerializableDictionaryList(List<SerializableKeyValuePair> data)
			{
				Data = data;
			}
		}

		[System.Serializable]
		private class SerializableList
		{
			public List<string> Data = null;

			public SerializableList(List<string> data)
			{
				Data = data;
			}
		}

		[System.Serializable]
		private class SerializableList<T>
		{
			public List<T> Data = null;

			public SerializableList(List<T> data)
			{
				Data = data;
			}
		}

		public static byte[] SerializeList<T>(this ISerializer serializer, IList<T> collection) where T : ISerializable, new()
		{
			var enumerator = collection.GetEnumerator();

			var list = new List<string>();

			while (enumerator.MoveNext())
			{
				var obj = enumerator.Current;
				var data = Encoding.Default.GetString(obj.Serialize(serializer));
				list.Add(data);
			}

			return serializer.Serialize(new SerializableList(list));
		}

		public static U DeserializeList<T, U>(this ISerializer serializer, byte[] data) where T : ISerializable, new() where U : IList<T>, new()
		{
			var list = serializer.Deserialize<SerializableList>(data).Data;
			
			var result = new U();

			if (list != null)
			{
				for (int idx = 0; idx < list.Count; ++idx)
				{
					var bytes = Encoding.Default.GetBytes(list[idx]);
					
					var item = new T();
					item.Deserialize(serializer, bytes);
					
					result.Add(item);
				}
			}

			return result;
		}

		public static void DeserializeList<T, U>(this ISerializer serializer, byte[] data, U collection) where T : ISerializable, new() where U : IList<T>
		{
			var list = serializer.Deserialize<SerializableList>(data).Data;

			if (list != null)
			{
				for (int idx = 0; idx < list.Count; ++idx)
				{
					var bytes = Encoding.Default.GetBytes(list[idx]);

					if (idx < collection.Count)
					{
						collection[idx].Deserialize(serializer, bytes);
					}
					else
					{
						var item = new T();
						item.Deserialize(serializer, bytes);
						
						collection.Add(item);
					}

				}
			}
		}

		public static byte[] SerializeDictionary(this ISerializer serializer, IDictionary<string, string> dictionary)
		{
			var list = new List<SerializableKeyValuePair>();

			foreach (var record in dictionary)
			{
				var pair = new SerializableKeyValuePair();
				
				pair.Key = record.Key;
				pair.Value = record.Value;

				list.Add(pair);
			}

			return serializer.Serialize(new SerializableDictionaryList(list));
		}

		public static U DeserializeDictionary<U>(this ISerializer serializer, byte[] data) where U : IDictionary<string, string>, new()
		{
			var list = serializer.Deserialize<SerializableDictionaryList>(data).Data;

			U result = new U();

			if (list != null)
			{
				foreach (var record in list)
				{
					result.Add(record.Key, record.Value);
				}
			}

			return result;
		}

		public static void DeserializeDictionary<U>(this ISerializer serializer, byte[] data, U dictionary) where U : IDictionary<string, string>
		{
			var list = serializer.Deserialize<SerializableDictionaryList>(data).Data;

			if (list != null)
			{
				foreach (var record in list)
				{
					if (dictionary.ContainsKey(record.Key))
					{
						dictionary[record.Key] = record.Value;
					}
					else
					{
						dictionary.Add(record.Key, record.Value);
					}
				}
			}
		}

		public static byte[] SerializeDictionary<T>(this ISerializer serializer, IDictionary<string, T> dictionary) where T : ISerializable, new()
		{
			var list = new List<SerializableKeyValuePair>();

			foreach (var record in dictionary)
			{
				var pair = new SerializableKeyValuePair();

				pair.Key = record.Key;
				pair.Value = Encoding.Default.GetString(record.Value.Serialize(serializer));

				list.Add(pair);
			}

			return serializer.Serialize(new SerializableDictionaryList(list));
		}

		public static U DeserializeDictionary<T, U>(this ISerializer serializer, byte[] data) where U : IDictionary<string, T>, new() where T : ISerializable, new()
		{
			var list = serializer.Deserialize<SerializableDictionaryList>(data).Data;

			U result = new U();

			if (list != null)
			{
				foreach (var record in list)
				{
					T value = new T();
					value.Deserialize(serializer, Encoding.Default.GetBytes(record.Value));

					result.Add(record.Key, value);
				}
			}

			return result;
		}

		public static void DeserializeDictionary<T, U>(this ISerializer serializer, byte[] data, U dictionary) where U : IDictionary<string, T> where T : ISerializable, new()
		{
			var list = serializer.Deserialize<SerializableDictionaryList>(data).Data;

			if (list != null)
			{
				foreach (var record in list)
				{
					if (dictionary.ContainsKey(record.Key))
					{
						dictionary[record.Key].Deserialize(serializer, Encoding.Default.GetBytes(record.Value));
					}
					else
					{
						T value = new T();
						value.Deserialize(serializer, Encoding.Default.GetBytes(record.Value));

						dictionary.Add(record.Key, value);
					}
				}
			}
		}
	}
}
