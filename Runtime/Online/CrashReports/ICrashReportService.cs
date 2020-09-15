#if REF_ONLINE_CRASH_REPORT

namespace REF.Runtime.Online.CrashReports
{
	public interface ICrashReportService : IOnlineService
	{
		void SetUserId(string userId);
		void SetCustomData(string key, string value);

		void Log(string message);
		void LogException(System.Exception exception);
	}
}

#endif
