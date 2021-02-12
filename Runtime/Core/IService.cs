
namespace REF.Runtime.Core
{
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

	public interface IService<TConfig> : IService where TConfig : IConfiguration
	{
		void Configure(TConfig config);
	}
}
