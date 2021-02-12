using System;
using System.Collections.Generic;

namespace REF.Runtime.Core
{
	public class ConfigServicePair
	{
		private IConfigInjector injector;

		public ConfigServicePair(IService service, IConfigInjector configInjector)
		{
			Service = service;
			injector = configInjector;
		}

		public IConfigInjector Config { get; private set; }
		public IService Service { get; private set; }
	}

	public interface IApp
	{
		string Build { get; set; }
		float Progress { get; }
		string Version { get; set; }

		event Action OnInitialized;

		bool Has<T>() where T : IService;
		bool IsInitialized();
		bool IsInitialized<T>() where T : IService;
		bool IsSupported<T>() where T : IService;

		void Set(IEnumerable<ConfigServicePair> pairList);

		IService Get(int idx);
		T Get<T>() where T : IService;
		IEnumerable<IService> GetAll();
		int GetServiceCount();

		void Initialize();
		void Release();
	}
}