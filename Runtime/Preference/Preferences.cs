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

		private List<IPreferenceable> preferenceObjects = new List<IPreferenceable>();

		public void Save()
		{
			for (int idx = 0; idx < preferenceObjects.Count; ++idx)
			{
				var obj = preferenceObjects[idx];
				
				var data = obj.Serialize(serializer);
				var key = obj.GetSerializationKey();

				saver.Save(key, data);
			}
		}

		public void Load()
		{
			for (int idx = 0; idx < preferenceObjects.Count; ++idx)
			{
				var obj = preferenceObjects[idx];
				var key = obj.GetSerializationKey();

				var data = saver.Load(key);
				obj.Deserialize(serializer, data);
			}
		}

		public void Register(IPreferenceable obj)
		{
			if (!preferenceObjects.Contains(obj))
			{
				preferenceObjects.Add(obj);
			}
		}

		public void Unregister(IPreferenceable obj)
		{
			if (preferenceObjects.Contains(obj))
			{
				preferenceObjects.Remove(obj);
			}
		}
	}
}
