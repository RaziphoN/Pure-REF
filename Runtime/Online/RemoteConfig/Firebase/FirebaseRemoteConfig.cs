#if REF_ONLINE_REMOTE_CONFIG && REF_FIREBASE_REMOTE_CONFIG && REF_USE_FIREBASE

using System.Collections.Generic;

using REF.Runtime.Core;
using REF.Runtime.Diagnostic;
using REF.Runtime.Online.Service;

using Firebase.Extensions;

namespace REF.Runtime.Online.RemoteConfig
{
	public class FirebaseRemoteConfig : FirebaseService, IRemoteConfigService
	{
		public event System.Action<IConfig> OnConfigFetched;
		public event System.Action OnConfigFetchFailed;

		private IConfig config;

		public IConfig Config { get { return config; } }

		public override void Configure(IConfiguration config)
		{
			base.Configure(config);

			var configuration = config as IRemoteConfigConfiguration;

			if (configuration == null)
			{
				RefDebug.Error(nameof(FirebaseRemoteConfig), $"Config must be of type {nameof(IRemoteConfigConfiguration)}!");
				return;
			}

			this.config = configuration.GetConfig();
		}

		public void Fetch(System.Action callback = null)
		{
			if (!FirebaseInitializer.AllowApiCalls)
			{
				OnConfigFetchFailed?.Invoke();
				callback?.Invoke();
				return;
			}

			System.Threading.Tasks.Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.FetchAsync();
			fetchTask.ContinueWithOnMainThread((task) =>
			{
				Apply(task);
				callback?.Invoke();
			});
		}

		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			UpdateFirebaseLocalDefaults();
			UpdateLocalConfig(); // Firebase save it's last fetched config internally, so we force to update our on init to match last fetched remote config

			if (successful)
			{
				Fetch(callback);
			}
			else
			{
				callback?.Invoke();
			}
		}

		private void Apply(System.Threading.Tasks.Task fetchTask)
		{
			bool isOk = Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
			if (isOk)
			{
				UpdateLocalConfig();
				OnConfigFetched?.Invoke(Config);
			}
			else
			{
				OnConfigFetchFailed?.Invoke();
			}
		}

		private void UpdateValue(Value to, Firebase.RemoteConfig.ConfigValue from)
		{
			switch (to.GetValueType())
			{
				case Type.Bool:
					to.SetBool(from.BooleanValue);
				break;

				case Type.Long:
					to.SetLong(from.LongValue);
				break;

				case Type.Double:
					to.SetDouble(from.DoubleValue);
				break;

				case Type.String:
					to.SetString(from.StringValue);
				break;

				default:
					break;
			}
		}

		private IDictionary<string, object> GetFirebaseApllicableConfig()
		{
			IDictionary<string, object> result = new Dictionary<string, object>();
			IDictionary<string, Value> config = Config.GetData();

			foreach (var prop in config)
			{
				switch (prop.Value.GetValueType())
				{
					case Type.Bool:
						result.Add(prop.Key, prop.Value.GetBool());
					break;

					case Type.Long:
						result.Add(prop.Key, prop.Value.GetLong());
					break;

					case Type.Double:
						result.Add(prop.Key, prop.Value.GetDouble());
					break;

					case Type.String:
						result.Add(prop.Key, prop.Value.GetString());
					break;

					default:
						break;
				}
			}

			return result;
		}

		private void UpdateFirebaseLocalDefaults()
		{
			if (IsInitialized())
			{
				Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(GetFirebaseApllicableConfig());
			}
		}

		private void UpdateLocalConfig()
		{
			if (FirebaseInitializer.AllowApiCalls)
			{
				IEnumerable<string> keys = Firebase.RemoteConfig.FirebaseRemoteConfig.Keys;

				foreach (var key in keys)
				{
					Value value = Config.GetValue(key);
					if (value != null)
						UpdateValue(value, Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(key));
				}
			}
		}
	}
}

#endif