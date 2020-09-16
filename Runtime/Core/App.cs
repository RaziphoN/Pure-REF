using UnityEngine;

using System.Collections;
using System.Collections.Generic;

namespace REF.Runtime.Core
{
	public abstract class App : MonoBehaviour
	{
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
				}

				// init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.Initialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);
				}

				// post-init
				for (int idx = 0; idx < supportedServices.Count; ++idx)
				{
					var service = supportedServices[idx];
					bool ended = false;
					service.PostInitialize(() => { ended = true; });
					yield return new WaitUntil(() => ended);
				}
			}
		}

		private void OnApplicationFocus(bool focus)
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

		private void OnApplicationPause(bool pause)
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

		private void OnApplicationQuit()
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

		private void OnDestroy()
		{
			for (int idx = services.Length - 1; idx < services.Length; --idx)
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
