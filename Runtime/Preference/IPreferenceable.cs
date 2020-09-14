using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	public interface IPreferenceable
	{
		string GetSerializationKey();

		byte[] Serialize(ISerializer serializer);
		void Deserialize(ISerializer serializer, byte[] data);
	}
}
