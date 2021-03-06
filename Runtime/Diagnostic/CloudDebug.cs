﻿using UnityEngine;
using UnityEngine.CrashReportHandler;

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
		[SerializeField] private Level stackTraceLevel = Level.Error;

#if REF_ONLINE_CRASH_REPORT
		private ICloudDebugService service;
#endif

		public void SetUserId(string userId)
		{
#if REF_ONLINE_CRASH_REPORT
			service?.SetUserId(userId);
#endif
			CrashReportHandler.SetUserMetadata("userId", userId);
		}

		public void SetCustomData(string key, string value)
		{
#if REF_ONLINE_CRASH_REPORT
			service?.SetCustomData(key, value);
#endif
			CrashReportHandler.SetUserMetadata(key, value);
		}


#if REF_ONLINE_CRASH_REPORT

		public void Construct(ICloudDebugService service)
		{
			this.service = service;
		}
#endif

		public void Initialize()
		{
			CrashReportHandler.logBufferSize = 20;
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

			if (logLevel > level || level == Level.None)
			{
				return;
			}

			if (enabled && service.IsInitialized())
			{
				service.Log($"[{LogLevelToPrefix(logLevel)}][{System.DateTime.Now.ToString("HH:mm:ss")}] {condition}");

				if (logLevel <= stackTraceLevel)
				{
					service.Log(stackTrace);
				}
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
