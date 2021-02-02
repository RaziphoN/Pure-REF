using System;
using System.Collections.Generic;

namespace REF.Runtime.Core
{
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

		void Set(IEnumerable<IService> serviceList);
		void Add(IService service);
		void Remove(IService service);

		IService Get(int idx);
		T Get<T>() where T : IService;
		IEnumerable<IService> GetAll();
		int GetServiceCount();

		void Initialize();
		void Release();
	}
}