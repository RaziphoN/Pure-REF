namespace REF.Runtime.Preference
{
	// The difference is that ISaveable is itself responsible to save/load data from whatever source of data whenever ISerializable is just define functionality to serialize itself to be saved somewhere
	public interface ISaveable
	{
		void Save();
		void Load();
	}
}
