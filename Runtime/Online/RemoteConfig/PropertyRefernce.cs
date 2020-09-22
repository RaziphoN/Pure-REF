#if REF_ONLINE_REMOTE_CONFIG

using REF.Runtime.Core;

namespace REF.Runtime.Online.RemoteConfig
{
	[System.Serializable]
	public class RemoteValueReference
	{
		public event System.Action<Value> OnValueChanged;

		private string key = string.Empty;
		private Value cachedValue = null;
		private IRemoteConfigService source = null;

		public static RemoteValueReference Create(string key, IRemoteConfigService source)
		{
			var reference = new RemoteValueReference(key, source);
			reference.Assign(source.Config);
			source.OnConfigFetched += reference.OnConfigChanged;

			return reference;
		}

		public static void Remove(RemoteValueReference reference)
		{
			reference.source.OnConfigFetched -= reference.OnConfigChanged;
		}

		private RemoteValueReference(string key, IRemoteConfigService source)
		{
			this.key = key;
			this.source = source;
		}

		public void Link(string key)
		{
			this.key = key;
		}

		public Value GetValue()
		{
			return cachedValue;
		}

		public bool GetBool(bool fallback = false)
		{
			var value = GetValue();

			if (value != null && value.GetValueType() == Type.Bool)
			{
				return value.GetBool();
			}

			return fallback;
		}

		public int GetInt(int fallback = 0)
		{
			var value = GetValue();
			
			if (value != null && value.GetValueType() == Type.Long)
			{
				return value.GetInt();
			}

			return fallback;
		}

		public double GetDouble(double fallback = 0d)
		{
			var value = GetValue();

			if (value != null && value.GetValueType() == Type.Double)
			{
				return value.GetDouble();
			}

			return fallback;
		}

		public string GetString(string fallback = "")
		{
			var value = GetValue();

			if (value != null && value.GetValueType() == Type.String)
			{
				return value.GetString();
			}

			return fallback;
		}

		private bool Assign(IConfig config)
		{
			if (config != null && config.HasKey(key))
			{
				var value = config.GetValue(key);

				if (cachedValue == null || !cachedValue.Equals(value))
				{
					cachedValue = value.Clone();
					return true;
				}
			}

			return false;
		}

		private void OnConfigChanged(IConfig config)
		{
			if (Assign(config))
			{
				OnValueChanged?.Invoke(cachedValue);
			}
		}
	}
}

#endif
