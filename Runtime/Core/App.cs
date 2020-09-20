using UnityEngine;

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

		// TODO: Add here your services to services array (initialization happens at Start, so managers could initialize something in Awake in case of MonoBehaviour)
		protected abstract void Assign();

		public static App Instance { get { return instance; } }

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
				RefDebug.SetColor(Color.red);

				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.PreInitialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);

					this.Log($"[{service.GetType().Name}] - PreInitialized");
				}

				OnPreInitialized?.Invoke();

				// init
				RefDebug.SetColor(Color.yellow);

				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.Initialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);

					this.Log($"[{service.GetType().Name}] - Initialized");
				}

				OnInitialized?.Invoke();

				// post-init
				RefDebug.SetColor(Color.green);

				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.PostInitialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);

					this.Log($"[{service.GetType().Name}] - PostInitialized");
				}

				RefDebug.SetColor(Color.white);

				OnPostInitialized?.Invoke();
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
