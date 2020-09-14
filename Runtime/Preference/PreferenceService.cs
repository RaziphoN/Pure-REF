using UnityEngine;

using REF.Runtime.Core;
using System;

namespace REF.Runtime.Preference
{
	public class PreferenceService : ServiceBase, IPreferenceService
	{
		[SerializeField] private Preferences preferences;

		public void Load()
		{
			preferences.Load();
		}

		public void Save()
		{
			preferences.Save();
		}

		public void Register(IPreferenceable obj)
		{
			preferences.Register(obj);
		}

		public void Unregister(IPreferenceable obj)
		{
			preferences.Unregister(obj);
		}
	}
}
