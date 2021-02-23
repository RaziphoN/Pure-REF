using System.Collections;

namespace REF.Runtime.Core
{
	public interface IApp
	{
		string Version { get; }
		float Progress { get; }

		event System.Action OnInitialized;

		bool Has<T>() where T : IService;
		bool IsInitialized();
		bool IsInitialized<T>() where T : IService;
		bool IsSupported<T>() where T : IService;

		T Get<T>() where T : IService;

		// NOTE: Register all services before initialization
		void Register(IConfiguration configuration, IService service);

		IEnumerator Initialize(string version, System.Action callback);
		IEnumerator Release(System.Action callback);

		void Update();

		void Suspend();
		void Resume();
	}
}