using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	public interface IPreferences : ISaver, ISaveable
	{
		ISerializer GetSerializer();

		void Register(string key, ISaveable obj);
		void Register(string key, ISerializable obj);
		void Unregister(string key);
	}
}
