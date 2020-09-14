using UnityEngine;

using REF.Runtime.Online.Service;

using Firebase.Crashlytics;

namespace REF.Runtime.Online.CrashReports
{
	[CreateAssetMenu(fileName = "FirebaseCrashlyticsService", menuName = "REF/Online/Crash Report/Firebase Crashlytics")]
	public class FirebaseCrashReportService : FirebaseService, ICrashReportService
	{
		public void Log(string message)
		{
			if (FirebaseInitializer.AllowApiCalls)
			{
				Crashlytics.Log(message);
			}
		}

		public void LogException(System.Exception exception)
		{
			if (FirebaseInitializer.AllowApiCalls)
			{
				Crashlytics.LogException(exception);
			}
		}

		public void SetCustomData(string key, string value)
		{
			if (FirebaseInitializer.AllowApiCalls)
			{
				Crashlytics.SetCustomKey(key, value);
			}
		}

		public void SetUserId(string userId)
		{
			if (FirebaseInitializer.AllowApiCalls)
			{
				Crashlytics.SetUserId(userId);
			}
		}

		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			callback?.Invoke();
		}
	}
}
