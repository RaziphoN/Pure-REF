namespace REF.Runtime.Serialization
{
	public interface ISerializer
	{
		string Serialize(Record record);
		Record Deserialize(string data);

		string Serialize(ISerializable obj);
		
		void Deserialize(string data, ISerializable obj);
		T Deserialize<T>(string data) where T : ISerializable, new();
	}
}
