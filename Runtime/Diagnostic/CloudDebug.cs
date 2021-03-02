using UnityEngine;

#if REF_ONLINE_CRASH_REPORT
using REF.Runtime.Online.CrashReports;
#endif

namespace REF.Runtime.Diagnostic
{
	[System.Serializable]
	public class CloudDebug
	{
		[SerializeField] private bool enabled = true;
		[SerializeField] private Level level = Level.Log;

#if REF_ONLINE_CRASH_REPORT
		private ICloudDebugService service;
#endif

		public void SetUserId(string userId)
		{
#if REF_ONLINE_CRASH_REPORT
			service?.SetUserId(userId);
#endif
		}

		public void SetCustomData(string key, string value)
		{
#if REF_ONLINE_CRASH_REPORT
			service?.SetCustomData(key, value);
#endif
		}


#if REF_ONLINE_CRASH_REPORT

		public void Construct(ICloudDebugService service)
		{
			this.service = service;
		}
#endif

		public void Initialize()
		{
			Application.logMessageReceived += OnLogReceivedHandler;
		}

		public void Release()
		{
			Application.logMessageReceived -= OnLogReceivedHandler;
		}

		private void OnLogReceivedHandler(string condition, string stackTrace, LogType type)
		{
#if REF_ONLINE_CRASH_REPORT
			if (service == null)
			{
				return;
			}

			var logLevel = LogLevelExtension.FromUnityDebugLevel(type);

			if (level < logLevel || level == Level.None)
			{
				return;
			}

			if (enabled && service.IsInitialized())
			{
				service.Log($"[{LogLevelToPrefix(logLevel)}][{System.DateTime.Now.ToString("HH:mm:ss")}] {condition}");
			}
#endif
		}

		private string LogLevelToPrefix(Level level)
		{
			switch (level)
			{
				case Level.None:
				default:
				{
					return "[None]";
				}
				break;

				case Level.Log:
				{
					return "[Info]";
				}
				break;

				case Level.Warning:
				{
					return "[Warning]";
				}
				break;

				case Level.Error:
				{
					return "[Error]";
				}
				break;

				case Level.Assert:
				case Level.Exception:
				{
					return "[Fatal]";
				}
				break;
			}
		}
	}
}
