namespace REF.Runtime.Preference
{
	public interface ISaver
	{
		bool HasKey(string key);
		void Save(string key, byte[] data);
		byte[] Load(string key);
	}
}
