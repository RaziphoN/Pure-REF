using UnityEngine;

using System.Text;

namespace REF.Runtime.Preference
{
	[CreateAssetMenu(fileName = "UnitySaver", menuName = "REF/Game Data/Default Unity Saver")]
	public class UnitySaver : Saver
	{
		public override bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public override byte[] Load(string key)
		{
			if (PlayerPrefs.HasKey(key))
			{
				var str = PlayerPrefs.GetString(key);
				return Encoding.Default.GetBytes(str);
			}

			return new byte[0];
		}

		public override void Save(string key, byte[] data)
		{
			PlayerPrefs.SetString(key, Encoding.Default.GetString(data));
			PlayerPrefs.Save();
		}
	}
}
