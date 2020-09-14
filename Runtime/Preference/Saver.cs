using UnityEngine;

namespace REF.Runtime.Preference
{
	public abstract class Saver : ScriptableObject, ISaver
	{
		public abstract byte[] Load(string key);
		public abstract void Save(string key, byte[] data);
	}
}
