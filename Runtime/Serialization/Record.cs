using System.Linq;
using System.Collections.Generic;

namespace REF.Runtime.Serialization
{
	// TODO: Implement ICloneable, IEnumerable

	[System.Serializable]
	public class Record
	{
		public enum Type
		{
			Null,
			Bool,
			Number,
			String,
			List,
			Object
		}

		private Type type;
		private bool boolValue;
		private double numValue;
		private string strValue;
		private List<Record> list;
		private Dictionary<string, Record> obj;

		public Record this[int index]
		{
			get
			{
				return GetItem(index);
			}

			set
			{
				SetItem(index, value);
			}
		}

		public Record this[string key]
		{
			get
			{
				return GetField(key);
			}

			set
			{
				SetField(key, value);
			}
		}

		public static Record Create()
		{
			var record = Create(Type.Null);
			return record;
		}

		public static Record Create(Type type)
		{
			var record = new Record();
			record.type = type;

			switch (type)
			{
				case Type.Object:
				{
					record.obj = new Dictionary<string, Record>();
				}
				break;

				case Type.List:
				{
					record.list = new List<Record>();
				}
				break;
			}

			return record;
		}

		public static Record Create(bool value)
		{
			var record = Create(Type.Bool);
			record.boolValue = value;

			return record;
		}

		public static Record Create(int value)
		{
			var record = Create(Type.Number);
			record.numValue = value;

			return record;
		}

		public static Record Create(long value)
		{
			var record = Create(Type.Number);
			record.numValue = value;

			return record;
		}

		public static Record Create(float value)
		{
			var record = Create(Type.Number);
			record.numValue = value;

			return record;
		}

		public static Record Create(double value)
		{
			var record = Create(Type.Number);
			record.numValue = value;

			return record;
		}

		public static Record Create(string value)
		{
			var record = Create(Type.String);
			record.strValue = value;

			return record;
		}

		public static Record Create(IList<string> list)
		{
			var record = Create(Type.List);

			for (int idx = 0; idx < list.Count; ++idx)
			{
				var item = list[idx];
				record.Add(item);
			}

			return record;
		}

		public static Record Create(IDictionary<string, string> dictionary)
		{
			var record = Create(Type.Object);

			foreach (var item in dictionary)
			{
				record.AddField(item.Key, Create(item.Value));
			}

			return record;
		}

		public bool IsNull() { return type == Type.Null; }
		public bool IsBool() { return type == Type.Bool; }
		public bool IsNumber() { return type == Type.Number; }
		public bool IsString() { return type == Type.String; }
		public bool IsList() { return type == Type.List; }
		public bool IsObject() { return type == Type.Object; }

		public void SetBool(bool value)
		{
			if (type == Type.Bool)
			{
				boolValue = value;
			}
		}

		public bool GetBool()
		{
			if (type == Type.Bool)
			{
				return boolValue;
			}

			return false;
		}

		public void SetInt(int value)
		{
			if (type == Type.Number)
			{
				numValue = value;
			}
		}

		public int GetInt()
		{
			if (type == Type.Number)
			{
				return (int)numValue;
			}

			return 0;
		}

		public void SetLong(long value)
		{
			if (type == Type.Number)
			{
				numValue = value;
			}
		}

		public long GetLong()
		{
			if (type == Type.Number)
			{
				return (long)numValue;
			}

			return 0L;
		}

		public void SetFloat(float value)
		{
			if (type == Type.Number)
			{
				numValue = value;
			}
		}

		public float GetFloat()
		{
			if (type == Type.Number)
			{
				return (float)numValue;
			}

			return 0f;
		}

		public void SetDouble(double value)
		{
			if (type == Type.Number)
			{
				numValue = value;
			}
		}

		public double GetDouble()
		{
			if (type == Type.Number)
			{
				return numValue;
			}

			return 0d;
		}

		public void SetString(string value)
		{
			if (type == Type.String)
			{
				strValue = value;
			}
		}

		public string GetString()
		{
			if (type == Type.String)
			{
				return strValue;
			}

			return string.Empty;
		}

		/* List */
		public bool Contains(bool value)
		{
			if (type == Type.List)
			{
				var idx = IndexOf(value);
				return idx > -1;
			}

			return false;
		}

		public bool Contains(int value)
		{
			if (type == Type.List)
			{
				var idx = IndexOf(value);
				return idx > -1;
			}

			return false;
		}

		public bool Contains(long value)
		{
			if (type == Type.List)
			{
				var idx = IndexOf(value);
				return idx > -1;
			}

			return false;
		}

		public bool Contains(float value)
		{
			if (type == Type.List)
			{
				var idx = IndexOf(value);
				return idx > -1;
			}

			return false;
		}

		public bool Contains(double value)
		{
			if (type == Type.List)
			{
				var idx = IndexOf(value);
				return idx > -1;
			}

			return false;
		}

		public bool Contains(string value)
		{
			if (type == Type.List)
			{
				var idx = IndexOf(value);
				return idx > -1;
			}

			return false;
		}

		public bool Contains(Record record)
		{
			if (type == Type.List)
			{
				var idx = IndexOf(record);
				return idx > -1;
			}

			return false;
		}

		public void AddNull()
		{
			if (type == Type.List)
			{
				list.Add(Create());
			}
		}

		public void Add(bool value)
		{
			if (type == Type.List)
			{
				list.Add(Create(value));
			}
		}

		public void Add(int value)
		{
			if (type == Type.List)
			{
				list.Add(Create(value));
			}
		}

		public void Add(long value)
		{
			if (type == Type.List)
			{
				list.Add(Create(value));
			}
		}

		public void Add(float value)
		{
			if (type == Type.List)
			{
				list.Add(Create(value));
			}
		}

		public void Add(double value)
		{
			if (type == Type.List)
			{
				list.Add(Create(value));
			}
		}

		public void Add(string value)
		{
			if (type == Type.List)
			{
				list.Add(Create(value));
			}
		}

		public void Add(Record record)
		{
			if (type == Type.List)
			{
				list.Add(record);
			}
		}

		public void Remove(bool value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				RemoveEquals(refItem);
			}
		}

		public void Remove(int value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				RemoveEquals(refItem);
			}
		}

		public void Remove(long value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				RemoveEquals(refItem);
			}
		}

		public void Remove(float value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				RemoveEquals(refItem);
			}
		}

		public void Remove(double value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				RemoveEquals(refItem);
			}
		}

		public void Remove(string value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				RemoveEquals(refItem);
			}
		}

		public void Remove(Record record)
		{
			if (type == Type.List)
			{
				RemoveEquals(record);
			}
		}

		public void RemoveAt(int index)
		{
			if (type == Type.List)
			{
				list.RemoveAt(index);
			}
		}

		public int IndexOf(bool value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				return IndexOfEquals(refItem);
			}

			return -1;
		}

		public int IndexOf(int value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				return IndexOfEquals(refItem);
			}

			return -1;
		}

		public int IndexOf(long value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				return IndexOfEquals(refItem);
			}

			return -1;
		}

		public int IndexOf(float value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				return IndexOfEquals(refItem);
			}

			return -1;
		}

		public int IndexOf(double value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				return IndexOfEquals(refItem);
			}

			return -1;
		}

		public int IndexOf(string value)
		{
			if (type == Type.List)
			{
				var refItem = Create(value);
				return IndexOfEquals(refItem);
			}

			return -1;
		}

		public int IndexOf(Record record)
		{
			if (type == Type.List)
			{
				return IndexOfEquals(record);
			}

			return -1;
		}

		public void Clear()
		{
			if (type == Type.List)
			{
				list.Clear();
			}
		}

		public void SetItem(int idx, Record record)
		{
			if (type == Type.List)
			{
				list[idx] = record;
			}
		}

		public Record GetItem(int idx)
		{
			if (type == Type.List)
			{
				return list[idx];
			}

			return null;
		}

		/* End of List */

		
		/* Object */

		public bool ContainsField(string key)
		{
			if (type == Type.Object)
			{
				return obj.ContainsKey(key);
			}

			return false;
		}

		public void AddField(string key, bool value)
		{
			if (type == Type.Object)
			{
				AddField(key, Create(value));
			}
		}

		public void AddField(string key, int value)
		{
			if (type == Type.Object)
			{
				AddField(key, Create(value));
			}
		}

		public void AddField(string key, long value)
		{
			if (type == Type.Object)
			{
				AddField(key, Create(value));
			}
		}

		public void AddField(string key, float value)
		{
			if (type == Type.Object)
			{
				AddField(key, Create(value));
			}
		}

		public void AddField(string key, double value)
		{
			if (type == Type.Object)
			{
				AddField(key, Create(value));
			}
		}

		public void AddField(string key, string value)
		{
			if (type == Type.Object)
			{
				AddField(key, Create(value));
			}
		}

		public void AddField(string key, Record record)
		{
			if (type == Type.Object)
			{
				obj.Add(key, record);
			}
		}

		public void RemoveField(string key)
		{
			if (type == Type.Object)
			{
				obj.Remove(key);
			}
		}

		public void SetField(string key, Record record)
		{
			if (type == Type.Object)
			{
				obj[key] = record;
			}
		}

		public Record GetField(string key)
		{
			if (type == Type.Object)
			{
				return obj[key];
			}

			return null;
		}

		public List<string> GetKeys()
		{
			if (type == Type.Object)
			{
				return obj.Keys.ToList();
			}

			return null;
		}

		/* End of Object */

		public int GetCount()
		{
			if (type == Type.List)
			{
				return list.Count;
			}

			if (type == Type.Object)
			{
				return obj.Count;
			}

			return 0;
		}

		public Record Clone()
		{
			switch (type)
			{
				case Type.Null:
				{
					return Create();
				}
				break;

				case Type.Bool:
				{
					return Create(boolValue);
				}
				break;

				case Type.Number:
				{
					return Create(numValue);
				}
				break;

				case Type.String:
				{
					return Create(strValue);
				}
				break;

				case Type.List:
				{
					var record = Create(Type.List);

					for (int idx = 0; idx < list.Count; ++idx)
					{
						var item = list[idx];
						record.Add(item.Clone());
					}

					return record;
				}
				break;

				case Type.Object:
				{
					var record = Create(Type.Object);

					foreach (var tuple in obj)
					{
						record.AddField(tuple.Key, tuple.Value.Clone());
					}

					return record;
				}
				break;
			}

			return null;
		}

		public override bool Equals(object obj)
		{
			var record = obj as Record;
			if (record == null)
			{
				return false;
			}

			return Equals(record);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public bool Equals(Record other)
		{
			if (type != other.type)
			{
				return false;
			}

			switch (type)
			{
				case Type.Null:
				{
					return true;
				}
				break;
				
				case Type.Bool:
				{
					return boolValue == other.boolValue;
				}
				break;
				
				case Type.Number:
				{
					return numValue == other.numValue;
				}
				break;
				
				case Type.String:
				{
					return strValue == other.strValue;
				}
				break;
				
				case Type.List:
				{
					if (list.Count != other.list.Count)
					{
						return false;
					}

					for (int idx = 0; idx < list.Count; ++idx)
					{
						var item = list[idx];
						var otherItem = other.list[idx];

						if (!item.Equals(otherItem))
						{
							return false;
						}
					}

					return true;
				}
				break;
				
				case Type.Object:
				{
					if (obj.Count != other.obj.Count)
					{
						return false;
					}

					foreach (var record in obj)
					{
						var key = record.Key;

						if (!other.ContainsField(key))
						{
							return false;
						}

						var otherField = other[key];
						if (!record.Value.Equals(otherField))
						{
							return false;
						}
					}
				}
				break;
			}

			return false;
		}

		private Record()
		{
			
		}

		private int IndexOfEquals(Record record)
		{
			if (type == Type.List)
			{
				for (int idx = 0; idx < list.Count; ++idx)
				{
					var item = list[idx];

					if (item.Equals(record))
					{
						return idx;
					}
				}
			}

			return -1;
		}

		private void RemoveEquals(Record record)
		{
			var idx = IndexOfEquals(record);

			if (idx > -1)
			{
				list.RemoveAt(idx);
			}
		}
	}
}
