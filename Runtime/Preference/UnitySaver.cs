using UnityEngine;

using System.Text;

namespace REF.Runtime.Preference
{
	public class UnitySaver : Saver
	{
		public override byte[] Load(string key)
		{
			if (PlayerPrefs.HasKey(key))
			{
				var str = PlayerPrefs.GetString(key);
				return Encoding.Default.GetBytes(str);
			}

			return null;
		}

		public override void Save(string key, byte[] data)
		{
			PlayerPrefs.SetString(key, Encoding.Default.GetString(data));
		}
	}
}
