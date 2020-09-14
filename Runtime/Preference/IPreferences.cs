namespace REF.Runtime.Preference
{
	public interface IPreferences
	{
		void Register(IPreferenceable obj);
		void Unregister(IPreferenceable obj);

		void Load();
		void Save();
	}
}
