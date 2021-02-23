using System.Collections.Generic;

using REF.Runtime.Core;
using REF.Runtime.Diagnostic;
using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	public class PreferenceService : ServiceBase, IPreferenceService
	{
		private ISaver saver;
		private ISerializer serializer;

		private IDictionary<string, ISaveable> saveables = new Dictionary<string, ISaveable>();
		private IDictionary<string, ISerializable> serializables = new Dictionary<string, ISerializable>();

		public override void Configure(IConfiguration config)
		{
			base.Configure(config);

			var configuration = config as IPreferenceServiceConfiguration;

			if (configuration == null)
			{
				RefDebug.Error(nameof(PreferenceService), $"Config must be of type {nameof(IPreferenceServiceConfiguration)}!");
				return;
			}

			saver = configuration.GetSaver();
			serializer = configuration.GetSerializer();
		}

		public ISerializer GetSerializer()
		{
			return serializer;
		}

		public ISaver GetSaver()
		{
			return saver;
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
			else
			{
				saveables[key] = obj;
			}
		}

		public void Register(string key, ISerializable obj)
		{
			if (!serializables.ContainsKey(key))
			{
				serializables.Add(key, obj);
			}
			else
			{
				serializables[key] = obj;
			}
		}

		public void Unregister(string key)
		{
			serializables.Remove(key);
			saveables.Remove(key);
		}
	}
}
