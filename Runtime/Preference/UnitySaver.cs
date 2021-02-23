using UnityEngine;

namespace REF.Runtime.Preference
{
	public class UnitySaver : ISaver
	{
		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public void Save(string key, string data)
		{
			PlayerPrefs.SetString(key, data);
			PlayerPrefs.Save();
		}

		public string Load(string key)
		{
			if (PlayerPrefs.HasKey(key))
			{
				return PlayerPrefs.GetString(key);
			}

			return string.Empty;
		}
	}
}
