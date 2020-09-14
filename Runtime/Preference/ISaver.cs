namespace REF.Runtime.Preference
{
	public interface ISaver
	{
		void Save(string key, byte[] data);
		byte[] Load(string key);
	}
}
