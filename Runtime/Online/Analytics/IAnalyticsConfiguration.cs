#if REF_ONLINE_ANALYTICS

using REF.Runtime.Core;

namespace REF.Runtime.Online.Analytics
{
	public interface IAnalyticsConfiguration : IConfiguration
	{
		bool IsAutoLogEnabled();
		bool IsCollectAdvertisingId();
	}
}

#endif
