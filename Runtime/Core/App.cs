using UnityEngine;

using System.Linq;
using System.Collections;
using System.Collections.Generic;

using REF.Runtime.Diagnostic;

namespace REF.Runtime.Core
{
	public abstract class App : MonoBehaviour
	{
		public event System.Action OnPreInitialized;
		public event System.Action OnInitialized;
		public event System.Action OnPostInitialized;

		private static App instance;
		
		protected IService[] services = null;

		[SerializeField] private string version = string.Empty;
		[SerializeField] private string build = string.Empty;

		public float Progress { get; private set; } = 0f;
		public string Version { get { return version; } set { version = value; } }
		public string Build { get { return build; } set { build = value; } }

		// TODO: Add here your services to services array (initialization happens at Start, so managers could initialize something in Awake in case of MonoBehaviour)
		public abstract void Assign();

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
				for (int idx = 0; idx < services.Length; ++idx)
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

		private void Awake()
		{
			instance = this;
		}

		private IEnumerator Start()
		{
			Assign();

			if (services != null)
			{
				var supportedServices = new List<IService>();

				for (int idx = 0; idx < services.Length; ++idx)
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

					this.Log(Color.red, $"[{service.GetType().Name}] - PreInitialized");
					Progress = (((idx + 1) / (float)supportedServices.Count) * 0.33f);
				}

				OnPreInitialized?.Invoke();

				// init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.Initialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);

					this.Log(Color.yellow, $"[{service.GetType().Name}] - Initialized");
					Progress = 0.33f + (((idx + 1) / (float)supportedServices.Count) * 0.33f);
				}

				OnInitialized?.Invoke();

				// post-init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.PostInitialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);

					this.Log(Color.green, $"[{service.GetType().Name}] - PostInitialized");
					Progress = 0.66f + (((idx + 1) / (float)supportedServices.Count) * 0.33f);
				}

				Progress = 1f;

				OnPostInitialized?.Invoke();
			}
		}

		private void Update()
		{
			if (services != null)
			{
				for (int idx = 0; idx < services.Length; ++idx)
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
				for (int idx = 0; idx < services.Length; ++idx)
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
				for (int idx = 0; idx < services.Length; ++idx)
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
				for (int idx = 0; idx < services.Length; ++idx)
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
			if (services != null)
			{
				for (int idx = services.Length - 1; idx >= 0; --idx)
				{
					var service = services[idx];
					if (service.IsInitialized())
					{
						service.Release(null);
					}
				}
			}
		}
	}
}
