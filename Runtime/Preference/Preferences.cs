using UnityEngine;

using System.Collections.Generic;

using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	[System.Serializable]
	public class Preferences : IPreferences
	{
		[SerializeField] private Saver saver;
		[SerializeField] private Serializer serializer;

		private IDictionary<string, ISerializable> serializables = new Dictionary<string, ISerializable>();

		public ISerializer GetSerializer()
		{
			return serializer;
		}

		public bool HasKey(string key)
		{
			return saver.HasKey(key);
		}

		public void Save(string key, byte[] data)
		{
			saver.Save(key, data);
		}

		public byte[] Load(string key)
		{
			return saver.Load(key);
		}

		public void Save()
		{
			foreach (var serializable in serializables)
			{
				var obj = serializable.Value;
				var data = obj.Serialize(serializer);

				saver.Save(serializable.Key, data);
			}
		}

		public void Load()
		{
			foreach (var serializable in serializables)
			{
				var obj = serializable.Value;
				var data = saver.Load(serializable.Key);

				obj.Deserialize(serializer, data);
			}
		}

		public void Register(string key, ISerializable obj)
		{
			if (!serializables.ContainsKey(key))
			{
				serializables.Add(key, obj);
			}
		}

		public void Unregister(string key)
		{
			if (serializables.ContainsKey(key))
			{
				serializables.Remove(key);
			}
		}
	}
}
