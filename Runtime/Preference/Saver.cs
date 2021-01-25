using UnityEngine;

namespace REF.Runtime.Preference
{
	public abstract class Saver : ScriptableObject, ISaver
	{
		public abstract bool HasKey(string key);
		public abstract string Load(string key);
		public abstract void Save(string key, string data);
	}
}
