namespace REF.Runtime.Preference
{
	public interface ISaver
	{
		bool HasKey(string key);
		void Save(string key, string data);
		string Load(string key);
	}
}
