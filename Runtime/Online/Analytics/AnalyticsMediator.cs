#if REF_ONLINE_ANALYTICS

using System.Linq;
using System.Collections.Generic;

using REF.Runtime.Online.Service;

namespace REF.Runtime.Online.Analytics
{
	public class AnalyticsMediator : OnlineService
	{
		private List<IAnalyticService> services = new List<IAnalyticService>();
		private System.Action postponedCallback = null;

		public void Register(IAnalyticService provider)
		{
			if (!services.Contains(provider))
				services.Add(provider);
		}

		public bool IsValidEvent(string eventName)
		{
			return services.All((provider) => { return provider.IsValidEvent(eventName); });
		}

		public bool IsValidParameter(Parameter parameter)
		{
			return services.All((provider) => { return provider.IsValidParameter(parameter); });
		}

		public void SendEvent(string name, Parameter[] parameters)
		{
			services.ForEach((provider) =>
			{
				provider.SendEvent(name, parameters);
			});
		}

		public void SetUserId(string id)
		{
			services.ForEach((provider) =>
			{
				provider.SetUserId(id);
			});
		}

		public void SetScreenName(string name)
		{
			services.ForEach((provider) =>
			{
				provider.SetScreenName(name);
			});
		}

		public override bool IsSupported()
		{
			return services.Any((service) => { return service.IsSupported(); });
		}

		public override void PreInitialize(System.Action callback)
		{
			postponedCallback = callback;

			System.Threading.Thread thread = new System.Threading.Thread(() =>
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					bool initialized = false;
					service.PreInitialize(() =>
					{
						initialized = true;
					});

					do {}
					while (!initialized);
				}

				postponedCallback?.Invoke();
				postponedCallback = null;
			});

			thread.Start();
		}

		public override void Initialize(System.Action callback)
		{
			postponedCallback = callback;

			System.Threading.Thread thread = new System.Threading.Thread(() =>
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					bool initialized = false;
					service.Initialize(() =>
					{
						initialized = true;
					});

					do
					{ }
					while (!initialized);
				}

				postponedCallback?.Invoke();
				postponedCallback = null;
			});

			thread.Start();
		}

		public override void PostInitialize(System.Action callback)
		{
			postponedCallback = callback;

			System.Threading.Thread thread = new System.Threading.Thread(() =>
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					bool initialized = false;
					service.PostInitialize(() =>
					{
						initialized = true;
					});

					do
					{ }
					while (!initialized);
				}

				postponedCallback?.Invoke();
				postponedCallback = null;
			});

			thread.Start();
		}

		public override void Update()
		{
			base.Update();

			services.ForEach((provider) =>
			{
				provider.Update();
			});
		}

		public override void Release(System.Action callback)
		{
			postponedCallback = callback;

			System.Threading.Thread thread = new System.Threading.Thread(() =>
			{
				for (int idx = 0; idx < services.Count; ++idx)
				{
					var service = services[idx];
					bool initialized = false;
					service.PostInitialize(() =>
					{
						initialized = true;
					});

					do
					{ }
					while (!initialized);
				}

				SetInitialized(true);
				postponedCallback?.Invoke();
				postponedCallback = null;
			});

			thread.Start();
		}

		public override void OnApplicationFocus(bool focused)
		{
			base.OnApplicationFocus(focused);

			services.ForEach((provider) =>
			{
				provider.OnApplicationFocus(focused);
			});
		}

		public override void OnApplicationPause(bool pause)
		{
			base.OnApplicationPause(pause);

			services.ForEach((provider) =>
			{
				provider.OnApplicationFocus(pause);
			});
		}

		public override void OnApplicationQuit()
		{
			base.OnApplicationQuit();

			services.ForEach((provider) =>
			{
				provider.OnApplicationQuit();
			});
		}
	}
}

#endif