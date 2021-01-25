using UnityEngine;

using System.Collections.Generic;

namespace REF.Runtime.Serialization
{
	[CreateAssetMenu(fileName = "UnitySerializer", menuName = "REF/Game Data/Default Unity Serializer")]
	public class UnitySerializer : SerializerBase
	{
		[System.Serializable]
		private class UnitySerializableRecord
		{
			[SerializeField] private Record.Type type;
			[SerializeField] private bool boolValue;
			[SerializeField] private double numValue;
			[SerializeField] private string strValue;
			[SerializeField] private List<string> keys;
			[SerializeField] private List<UnitySerializableRecord> list;

			public UnitySerializableRecord(Record record)
			{
				if (record.IsNull())
				{
					type = Record.Type.Null;
				}

				if (record.IsBool())
				{
					type = Record.Type.Bool;
					boolValue = record.GetBool();
				}

				if (record.IsNumber())
				{
					type = Record.Type.Number;
					numValue = record.GetDouble();
				}

				if (record.IsString())
				{
					type = Record.Type.String;
					strValue = record.GetString();
				}

				if (record.IsList())
				{
					type = Record.Type.List;
					list = new List<UnitySerializableRecord>();

					for (int idx = 0; idx < record.GetCount(); ++idx)
					{
						var item = record[idx];
						list.Add(new UnitySerializableRecord(item));
					}
				}

				if (record.IsObject())
				{
					type = Record.Type.Object;
					keys = new List<string>();
					list = new List<UnitySerializableRecord>();

					var recordKeys = record.GetKeys();
					for (int idx = 0; idx < record.GetCount(); ++idx)
					{
						var key = recordKeys[idx];
						var field = record.GetField(key);

						keys.Add(key);
						list.Add(new UnitySerializableRecord(field));
					}
				}
			}

			public Record ToRecord()
			{
				switch (type)
				{
					case Record.Type.Null:
					{
						return Record.Create();
					}
					break;

					case Record.Type.Bool:
					{
						return Record.Create(boolValue);
					}
					break;

					case Record.Type.Number:
					{
						return Record.Create(numValue);
					}
					break;

					case Record.Type.String:
					{
						return Record.Create(strValue);
					}
					break;

					case Record.Type.List:
					{
						var record = Record.Create(Record.Type.List);

						for (int idx = 0; idx < list.Count; ++idx)
						{
							var item = list[idx];
							record.Add(item.ToRecord());
						}

						return record;
					}
					break;

					case Record.Type.Object:
					{
						var record = Record.Create(Record.Type.Object);

						for (int idx = 0; idx < keys.Count; ++idx)
						{
							var key = keys[idx];
							var item = list[idx];

							record.AddField(key, item.ToRecord());
						}

						return record;
					}
					break;
				}

				return null;
			}
		}

		public override string Serialize(Record record)
		{
			var serializableRecord = new UnitySerializableRecord(record);
			var json = JsonUtility.ToJson(serializableRecord);

			return json;
		}

		public override Record Deserialize(string data)
		{
			var record = (JsonUtility.FromJson<UnitySerializableRecord>(data)).ToRecord();
			return record;
		}

		public override string Serialize(ISerializable obj)
		{
			var record = obj.Serialize();
			return Serialize(record);
		}

		public override void Deserialize(string data, ISerializable obj)
		{
			var record = Deserialize(data);
			obj.Deserialize(record);
		}

		public override T Deserialize<T>(string data)
		{
			var record = Deserialize(data);

			T result = new T();
			result.Deserialize(record);

			return result;
		}
	}
}
