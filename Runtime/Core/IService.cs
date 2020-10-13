
namespace REF.Runtime.Core
{
	// if coroutine is required to initialize service, then you have to make a service that is inherit from MonoBehaviour and start a coroutine inside interface methods,
	// otherwise this functions is expected to be non-blocking calls
	public interface IService
	{
		bool IsSupported();
		bool IsInitialized();

		void PreInitialize(System.Action callback);
		void Initialize(System.Action callback);
		void PostInitialize(System.Action callback);

		void Update();

		void Release(System.Action callback);

		void OnApplicationFocus(bool focused);
		void OnApplicationPause(bool pause);

		void OnApplicationQuit();
	}
}
