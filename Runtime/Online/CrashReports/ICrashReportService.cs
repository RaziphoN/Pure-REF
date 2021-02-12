#if REF_ONLINE_CRASH_REPORT

using REF.Runtime.Core;

namespace REF.Runtime.Online.CrashReports
{
	public interface ICrashReportService : IOnlineService<IConfiguration>
	{
		void SetUserId(string userId);
		void SetCustomData(string key, string value);

		void Log(string message);
		void LogException(System.Exception exception);
	}
}

#endif
