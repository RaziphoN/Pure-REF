#if REF_ONLINE_REMOTE_CONFIG

namespace REF.Runtime.Online.RemoteConfig
{
	public interface IRemoteConfigService : IOnlineService
	{
		event System.Action<IConfig> OnConfigFetched;
		event System.Action OnConfigFetchFailed;

		IConfig Config { get; }

		void Fetch(System.Action callback = null);
	}
}

#endif
