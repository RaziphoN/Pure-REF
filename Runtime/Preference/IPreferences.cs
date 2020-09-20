using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	public interface IPreferences : ISaver
	{
		ISerializer GetSerializer();

		void Register(string key, ISerializable obj);
		void Unregister(string key);

		void Save();
		void Load();
	}
}
