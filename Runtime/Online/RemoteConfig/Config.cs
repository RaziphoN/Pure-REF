using System.Collections.Generic;

namespace REF.Runtime.Online.RemoteConfig
{
	public class Config : IConfig
	{
		private IDictionary<string, Value> data = new Dictionary<string, Value>();

		public void AddValue(string key, Value value)
		{
			if (!data.ContainsKey(key))
				data.Add(key, value);
		}

		public IDictionary<string, Value> GetData()
		{
			return data;
		}

		public IEnumerable<string> GetKeys()
		{
			return data.Keys;
		}

		public Value GetValue(string key)
		{
			if (data.ContainsKey(key))
				return data[key];

			return null;
		}

		public bool HasKey(string key)
		{
			return data.ContainsKey(key);
		}

		public void RemoveValue(string key)
		{
			if (data.ContainsKey(key))
				data.Remove(key);
		}

		public void SetValue(string key, Value value)
		{
			if (data.ContainsKey(key))
			{
				data[key] = value;
			}
		}
	}
}
