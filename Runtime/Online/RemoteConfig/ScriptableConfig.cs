using System.Collections.Generic;
using UnityEngine;

namespace REF.Runtime.Online.RemoteConfig
{
	[System.Serializable]
	public class ScriptableConfigProperty
	{
		[SerializeField] private string key;
		[SerializeField] private Value value;

		public ScriptableConfigProperty(string key, Value value)
		{
			this.key = key;
			this.value = value;
		}

		public void SetValue(Value value)
		{
			this.value = value;
		}

		public string GetKey()
		{
			return key;
		}

		public Value GetValue()
		{
			return value;
		}
	}

	[CreateAssetMenu(fileName = "Config", menuName = "REF/Configuration/Value Config", order = 0)]
	public class ScriptableConfig : ScriptableObject, IConfig
	{
		[SerializeField] private List<ScriptableConfigProperty> properties = new List<ScriptableConfigProperty>();

		public void AddValue(string key, Value value)
		{
			if (!HasKey(key))
			{
				properties.Add(new ScriptableConfigProperty(key, value));
			}
		}

		public IDictionary<string, Value> GetData()
		{
			IDictionary<string, Value> data = new Dictionary<string, Value>();

			properties.ForEach((property) =>
			{
				data.Add(property.GetKey(), property.GetValue());
			});

			return data;
		}

		public IEnumerable<string> GetKeys()
		{
			return properties.ConvertAll((property) =>
			{
				return property.GetKey();
			});
		}

		public Value GetValue(string key)
		{
			var property = GetProperty(key);

			if (property != null)
				return property.GetValue();

			return null;
		}

		public bool HasKey(string key)
		{
			var property = GetProperty(key);
			return property != null;
		}

		public void RemoveValue(string key)
		{
			var property = GetProperty(key);
			
			if (property != null)
				properties.Remove(property);
		}

		public void SetValue(string key, Value value)
		{
			var property = GetProperty(key);

			if (property != null)
				property.SetValue(value);
		}

		private ScriptableConfigProperty GetProperty(string key)
		{
			var property = properties.Find((prop) =>
			{
				return prop.GetKey().Equals(key);
			});

			return property;
		}
	}
}
