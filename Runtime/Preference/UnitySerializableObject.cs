using System.Collections.Generic;

namespace REF.Runtime.Preference
{
	[System.Serializable]
	public class SerializableField
	{
		public SerializableField(string key, byte[] data)
		{
			this.Key = key;
			this.Data = data;
		}

		public string Key;
		public byte[] Data;
	}

	[System.Serializable]
	public class UnitySerializableObject
	{
		public List<SerializableField> Fields = new List<SerializableField>();

		public SerializableField GetField(string key)
		{
			return Fields.Find((field) => { return field.Key == key; });
		}
	}
}
