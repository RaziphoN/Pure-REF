
namespace REF.Runtime.Core
{
	public interface IService
	{
		bool IsSupported();
		bool IsInitialized();

		void Construct(IApp app);
		void PreInitialize(System.Action callback);
		void Configure(IConfiguration configuration);
		void Initialize(System.Action callback);
		void PostInitialize(System.Action callback);

		void Update();

		void Suspend();
		void Resume();

		void Release(System.Action callback);
	}
}
