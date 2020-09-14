namespace REF.Runtime.Serialization
{
	public interface ISerializer
	{
		byte[] Serialize(object obj);
		
		void Deserialize(byte[] data, object obj);
		T Deserialize<T>(byte[] data);
	}
}
