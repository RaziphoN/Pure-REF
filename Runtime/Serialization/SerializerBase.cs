using UnityEngine;

namespace REF.Runtime.Serialization
{
	public abstract class SerializerBase : ScriptableObject, ISerializer
	{
		public abstract string Serialize(Record record);
		public abstract Record Deserialize(string data);

		public abstract string Serialize(ISerializable obj);
		public abstract void Deserialize(string data, ISerializable obj);
		public abstract T Deserialize<T>(string data) where T : ISerializable, new();
	}
}
