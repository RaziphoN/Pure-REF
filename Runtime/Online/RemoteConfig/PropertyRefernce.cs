#if REF_ONLINE_REMOTE_CONFIG

using System.Collections.Generic;

using REF.Runtime.Core;

namespace REF.Runtime.Online.RemoteConfig
{
	[System.Serializable]
	public class PropertyReference
	{
		private string key;

		public PropertyReference(string key)
		{
		}

		public void Link(string key)
		{
			this.key = key;
		}

		public Value GetValue()
		{
			if (App.Instance == null)
			{
				return null;
			}

			var remoteConfigService = App.Instance.Get<IRemoteConfigService>();
			if (remoteConfigService == null)
			{
				return null;
			}

			var config = remoteConfigService.Config;
			if (config == null)
			{
				return null;
			}

			if (!config.HasKey(key))
			{
				return null;
			}

			return config.GetValue(key);
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
	}
}

#endif
