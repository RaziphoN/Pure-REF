using UnityEngine;

namespace REF.Runtime.Online
{
	public enum Type
	{
		Bool,
		Long,
		Double,
		String,
	}

	[System.Serializable]
	public class Value
	{
		[SerializeField] private bool boolValue = false;
		[SerializeField] private long longValue = 0L;
		[SerializeField] private double doubleValue = 0D;
		[SerializeField] private string stringValue = string.Empty;
		[SerializeField] private Type type = Type.String;

		public Value(bool value)
		{
			SetBool(value);
		}

		public Value(int value)
		{
			SetInt(value);
		}

		public Value(long value)
		{
			SetLong(value);
		}

		public Value(float value)
		{
			SetFloat(value);
		}

		public Value(double value)
		{
			SetDouble(value);
		}

		public Value(string value)
		{
			SetString(value);
		}

		public Type GetValueType()
		{
			return type;
		}

		public void SetBool(bool value)
		{
			type = Type.Bool;
			boolValue = value;
		}

		public bool GetBool()
		{
			return boolValue;
		}

		public void SetInt(int value)
		{
			type = Type.Long;
			longValue = value;
		}

		public int GetInt()
		{
			return (int)longValue;
		}

		public void SetLong(long value)
		{
			type = Type.Long;
			longValue = value;
		}

		public long GetLong()
		{
			return longValue;
		}

		public void SetFloat(float value)
		{
			type = Type.Double;
			doubleValue = value;
		}

		public float GetFloat()
		{
			return (float)doubleValue;
		}

		public void SetDouble(double value)
		{
			type = Type.Double;
			doubleValue = value;
		}

		public double GetDouble()
		{
			return doubleValue;
		}

		public void SetString(string value)
		{
			type = Type.String;
			stringValue = value;
		}

		public string GetString()
		{
			return stringValue;
		}

		public override string ToString()
		{
			switch (type)
			{
				case Type.Bool:
					return boolValue.ToString();

				case Type.Long:
					return longValue.ToString();

				case Type.Double:
					return doubleValue.ToString();

				case Type.String:
					return stringValue;
			}

			return "Undefined";
		}
	}
}
