#if REF_ONLINE_REMOTE_CONFIG

using REF.Runtime.Core;

namespace REF.Runtime.Online.RemoteConfig
{
	public interface IRemoteConfigConfiguration : IConfiguration
	{
		IConfig GetConfig();
	}
}

#endif
