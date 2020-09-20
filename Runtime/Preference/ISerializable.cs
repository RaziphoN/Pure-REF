using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	public interface ISerializable
	{
		byte[] Serialize(ISerializer serializer);
		void Deserialize(ISerializer serializer, byte[] data);
	}
}
