using UnityEngine;

using REF.Runtime.Core;
using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	[CreateAssetMenu(fileName = "PreferenceService", menuName = "REF/Game Data/Preferences")]
	public class PreferenceService : ServiceBase, IPreferenceService
	{
		[SerializeField] private Preferences preferences;

		public ISerializer GetSerializer()
		{
			return preferences.GetSerializer();
		}

		public bool HasKey(string key)
		{
			return preferences.HasKey(key);
		}

		public void Save(string key, string data)
		{
			preferences.Save(key, data);
		}

		public string Load(string key)
		{
			return preferences.Load(key);
		}

		[ContextMenu("Load")]
		public void Load()
		{
			preferences.Load();
		}

		[ContextMenu("Save")]
		public void Save()
		{
			preferences.Save();
		}

		public void Register(string key, ISaveable obj)
		{
			preferences.Register(key, obj);
		}

		public void Register(string key, ISerializable obj)
		{
			preferences.Register(key, obj);
		}

		public void Unregister(string key)
		{
			preferences.Unregister(key);
		}
	}
}
