using UnityEngine;

namespace REF.Runtime.Serialization
{
	public abstract class Serializer : ScriptableObject, ISerializer
	{
		public abstract byte[] Serialize(object obj);
		public abstract void Deserialize(byte[] data, object obj);
		public abstract T Deserialize<T>(byte[] data);
	}
}
