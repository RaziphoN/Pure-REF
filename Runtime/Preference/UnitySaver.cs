using UnityEngine;

namespace REF.Runtime.Preference
{
	[CreateAssetMenu(fileName = "UnitySaver", menuName = "REF/Game Data/Default Unity Saver")]
	public class UnitySaver : Saver
	{
		public override bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public override string Load(string key)
		{
			if (PlayerPrefs.HasKey(key))
			{
				return PlayerPrefs.GetString(key);
			}

			return string.Empty;
		}

		public override void Save(string key, string data)
		{
			PlayerPrefs.SetString(key, data);
			PlayerPrefs.Save();
		}
	}
}
