#if REF_ONLINE_CRASH_REPORT

using REF.Runtime.Core;

namespace REF.Runtime.Online.CrashReports
{
	public interface ICloudDebugConfiguration : IConfiguration
	{
		bool IsCollectAutomatically();
	}
}

#endif
