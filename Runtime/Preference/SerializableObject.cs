using System.Collections.Generic;

namespace REF.Runtime.Preference
{
	[System.Serializable]
	public class SerializableField
	{
		public SerializableField(string key, string data)
		{
			this.Key = key;
			this.Data = data;
		}

		public string Key;
		public string Data;
	}

	[System.Serializable]
	public class SerializableObject
	{
		public List<SerializableField> Fields = new List<SerializableField>();

		public bool HasField(string key)
		{
			var field = GetField(key);
			return field != null;
		}

		public SerializableField GetField(string key)
		{
			return Fields.Find((field) => { return field.Key == key; });
		}
	}
}
