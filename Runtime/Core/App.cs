using System.Linq;
using System.Collections;
using System.Collections.Generic;

using REF.Runtime.Diagnostic;

namespace REF.Runtime.Core
{
	public class App : IApp
	{
		public event System.Action OnInitialized;

		private bool initialized;
		private List<IConfiguration> configs = new List<IConfiguration>();
		private List<IService> services = new List<IService>();

		public string Version { get; private set; }
		public float Progress { get; private set; }

		public bool IsInitialized()
		{
			return initialized;
		}

		public bool IsInitialized<T>() where T : IService
		{
			var service = Get<T>();

			if (service != null)
			{
				return service.IsInitialized();
			}

			return false;
		}

		public bool IsSupported<T>() where T : IService
		{
			var service = Get<T>();

			if (service != null)
			{
				return service.IsSupported();
			}

			return false;
		}

		public void Register(IConfiguration config, IService service)
		{
			services.Add(service);
			configs.Add(config);
		}

		public bool Has<T>() where T : IService
		{
			var service = Get<T>();

			if (service != null)
			{
				return true;
			}

			return false;
		}

		public T Get<T>() where T : IService
		{
			if (services == null)
			{
				return default(T);
			}

			if (services != null)
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];

					var genericType = typeof(T);
					var serviceType = service.GetType();

					if (genericType.IsAssignableFrom(serviceType))
					{
						return (T)service;
					}
				}
			}

			return default(T);
		}

		public IEnumerator Initialize(string version, System.Action callback)
		{
			Version = version;

			if (services != null)
			{
				var supportedServices = new List<IService>();

				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];

					if (service.IsSupported())
					{
						service.Construct(this);
						supportedServices.Add(service);
					}
				}

				// pre-init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.PreInitialize(() => { ended = true; });
					yield return WaitUntil(() => ended);

					Progress = (((idx + 1) / (float)supportedServices.Count) * 0.33f);
					RefDebug.Log(nameof(App), $"[{service.GetType().Name}] - PreInitialized {Progress * 100}");
				}

				// configure
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					var config = configs[idx]; // TODO: This idx MUST match if some service isn't supported
					service.Configure(config);

					RefDebug.Log(nameof(App), $"[{services[idx].GetType().Name}] - Configured");
				}

				// init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.Initialize(() => { ended = true; });
					yield return WaitUntil(() => ended);

					Progress = 0.33f + (((idx + 1) / (float)supportedServices.Count) * 0.33f);
					RefDebug.Log(nameof(App), $"[{service.GetType().Name}] - Initialized {Progress * 100}");
				}

				// post-init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.PostInitialize(() => { ended = true; });
					yield return WaitUntil(() => ended);

					Progress = 0.66f + (((idx + 1) / (float)supportedServices.Count) * 0.33f);
					RefDebug.Log(nameof(App), $"[{service.GetType().Name}] - PostInitialized {Progress * 100}");
				}

				Progress = 1f;

				initialized = true;
				callback?.Invoke();
				OnInitialized?.Invoke();
			}
		}

		public void Update()
		{
			if (services != null && IsInitialized())
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					//if (service.IsInitialized())
					{
						service.Update();
					}
				}
			}
		}

		public IEnumerator Release(System.Action callback)
		{
			if (services != null)
			{
				for (int idx = services.Count - 1; idx >= 0; --idx)
				{
					var service = services[idx];
					if (service.IsInitialized())
					{
						bool ended = false;
						service.Release(() => { ended = true; });
						yield return WaitUntil(() => ended);
						RefDebug.Log(nameof(App), $"[{service.GetType().Name}] - Released");
					}
				}
			}

			Progress = 0f;
			initialized = false;
			callback?.Invoke();
		}

		public void Suspend()
		{
			if (services != null && IsInitialized())
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					if (service.IsInitialized())
					{
						service.Suspend();
					}
				}
			}
		}

		public void Resume()
		{
			if (services != null && IsInitialized())
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					if (service.IsInitialized())
					{
						service.Resume();
					}
				}
			}
		}

		private IEnumerator WaitUntil(System.Func<bool> condition)
		{
			while(!condition.Invoke())
			{
				yield return null;
			}
		}
	}
}
