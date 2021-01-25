namespace REF.Runtime.Serialization
{
	public interface ISerializable
	{
		Record Serialize();
		void Deserialize(Record record);
	}
}
