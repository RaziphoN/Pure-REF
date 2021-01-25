using UnityEngine;

using System.Collections.Generic;

using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	[System.Serializable]
	public class Preferences : IPreferences
	{
		[SerializeField] private Saver saver;
		[SerializeField] private SerializerBase serializer;

		private IDictionary<string, ISaveable> saveables = new Dictionary<string, ISaveable>();
		private IDictionary<string, ISerializable> serializables = new Dictionary<string, ISerializable>();

		public ISerializer GetSerializer()
		{
			return serializer;
		}

		public bool HasKey(string key)
		{
			return saver.HasKey(key);
		}

		public void Save(string key, string data)
		{
			saver.Save(key, data);
		}

		public string Load(string key)
		{
			return saver.Load(key);
		}

		public void Save()
		{
			foreach (var serializable in serializables)
			{
				var obj = serializable.Value;
				var data = serializer.Serialize(obj);

				saver.Save(serializable.Key, data);
			}

			foreach (var saveable in saveables)
			{
				var obj = saveable.Value;
				obj.Save();
			}
		}

		public void Load()
		{
			foreach (var serializable in serializables)
			{
				var obj = serializable.Value;
				var data = saver.Load(serializable.Key);

				if (!string.IsNullOrEmpty(data))
				{
					serializer.Deserialize(data, obj);
				}
			}

			foreach (var saveable in saveables)
			{
				var obj = saveable.Value;
				obj.Load();
			}
		}

		public void Register(string key, ISaveable obj)
		{
			if (!saveables.ContainsKey(key))
			{
				saveables.Add(key, obj);
			}
		}

		public void Register(string key, ISerializable obj)
		{
			if (!serializables.ContainsKey(key))
			{
				serializables.Add(key, obj);
			}
		}

		public void UnregisterSerializable(string key)
		{
			serializables.Remove(key);
		}

		public void UnregisterSaveable(string key)
		{
			saveables.Remove(key);
		}

		public void Unregister(string key)
		{
			UnregisterSaveable(key);
			UnregisterSerializable(key);
		}
	}
}
