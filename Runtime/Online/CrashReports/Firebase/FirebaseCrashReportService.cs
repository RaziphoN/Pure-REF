#if REF_ONLINE_CRASH_REPORT && REF_FIREBASE_CRASH_REPORT && REF_USE_FIREBASE

using REF.Runtime.Core;
using REF.Runtime.Diagnostic;
using REF.Runtime.Online.Service;

using Firebase.Crashlytics;

namespace REF.Runtime.Online.CrashReports
{
	public class FirebaseCrashReportService : FirebaseService, ICloudDebugService
	{
		private bool autoCollectLogs = true;

		public override void Configure(IConfiguration config)
		{
			base.Configure(config);

			var configuration = config as ICloudDebugConfiguration;

			if (configuration == null)
			{
				RefDebug.Error(nameof(FirebaseCrashReportService), $"Config must be of type {nameof(ICloudDebugConfiguration)}!");
				return;
			}

			autoCollectLogs = configuration.IsCollectAutomatically();
		}

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
			Crashlytics.IsCrashlyticsCollectionEnabled = autoCollectLogs;
			callback?.Invoke();
		}
	}
}

#endif