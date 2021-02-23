using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	public interface IPreferences : ISaveable
	{
		ISerializer GetSerializer();
		ISaver GetSaver();

		void Register(string key, ISaveable obj);
		void Register(string key, ISerializable obj);
		void Unregister(string key);
	}
}
