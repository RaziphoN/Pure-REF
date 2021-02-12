using UnityEngine;

using System.Linq;
using System.Collections;
using System.Collections.Generic;

using REF.Runtime.Diagnostic;

namespace REF.Runtime.Core
{
	public abstract class App : MonoBehaviour, IApp
	{
		private static App instance;

		public event System.Action OnInitialized;

		[SerializeField] private bool autoInit = true;
		[SerializeField] private string version = string.Empty;
		[SerializeField] private string build = string.Empty;

		private List<IConfigInjector> configs = null;
		private List<IService> services = null;

		public float Progress { get; private set; } = 0f;
		public string Version { get { return version; } set { version = value; } }
		public string Build { get { return build; } set { build = value; } }

		public static App Instance { get { return instance; } }

		public bool IsInitialized()
		{
			var supported = services.Where((service) => { return service.IsSupported(); });
			return supported.All((service) => { return service.IsInitialized(); });
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

		public void Set(IEnumerable<ConfigServicePair> pairList)
		{
			var count = pairList.Count();
			services = new List<IService>(count);
			configs = new List<IConfigInjector>(count);

			foreach (var pair in pairList)
			{
				services.Add(pair.Service);
				configs.Add(pair.Config);
			}
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

		public IService Get(int idx)
		{
			return services[idx];
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

		public IEnumerable<IService> GetAll()
		{
			return services;
		}

		public int GetServiceCount()
		{
			return services.Count;
		}

		public void Initialize()
		{
			StartCoroutine(InitializeInternal());
		}

		public void Release()
		{
			if (services != null)
			{
				for (int idx = services.Count - 1; idx >= 0; --idx)
				{
					var service = services[idx];
					if (service.IsInitialized())
					{
						service.Release(null); // TODO: Coroutine to release
					}
				}
			}

			Progress = 0f;
		}

		protected void Awake()
		{
			instance = this;
		}

		private void Start()
		{
			if (autoInit)
			{
				StartCoroutine(InitializeInternal());
			}
		}

		private void Update()
		{
			if (services != null)
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					if (service.IsInitialized())
					{
						service.Update();
					}
				}
			}
		}

		private void OnApplicationFocus(bool focus)
		{
			if (services != null)
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					if (service.IsInitialized())
					{
						service.OnApplicationFocus(focus);
					}
				}
			}
		}

		private void OnApplicationPause(bool pause)
		{
			if (services != null)
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					if (service.IsInitialized())
					{
						service.OnApplicationPause(pause);
					}
				}
			}
		}

		private void OnApplicationQuit()
		{
			if (services != null)
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					if (service.IsInitialized())
					{
						service.OnApplicationQuit();
					}
				}
			}
		}

		private void OnDestroy()
		{
			if (autoInit)
			{
				Release();
			}
		}

		private IEnumerator InitializeInternal()
		{
			if (services != null)
			{
				var supportedServices = new List<IService>();

				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];

					if (service.IsSupported())
					{
						supportedServices.Add(service);
					}
				}

				// pre-init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.PreInitialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);

					Progress = (((idx + 1) / (float)supportedServices.Count) * 0.33f);
					this.Log($"[{service.GetType().Name}] - PreInitialized {Progress * 100}");
				}
				
				// configure
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var injector = configs[idx];
					injector?.Configure();

					this.Log($"[{services[idx].GetType().Name}] - Configured");
				}

				// init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.Initialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);

					Progress = 0.33f + (((idx + 1) / (float)supportedServices.Count) * 0.33f);
					this.Log($"[{service.GetType().Name}] - Initialized {Progress * 100}");
				}

				// post-init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.PostInitialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);

					Progress = 0.66f + (((idx + 1) / (float)supportedServices.Count) * 0.33f);
					this.Log($"[{service.GetType().Name}] - PostInitialized {Progress * 100}");
				}

				Progress = 1f;

				OnInitialized?.Invoke();
			}
		}
	}
}
