using UnityEngine;

namespace REF.Runtime.Diagnostic
{
	public enum Level
	{
		None = 0,
		Assert = 1,
		Exception = 2,
		Error = 3,
		Warning = 4,
		Log = 5,
	}

	public static class LogLevelExtension
	{
		public static Level FromUnityDebugLevel(LogType level)
		{
			switch (level)
			{
				case LogType.Error:
				{
					return Level.Error;
				}
				break;

				case LogType.Assert:
				{
					return Level.Assert;
				}
				break;

				case LogType.Warning:
				{
					return Level.Warning;
				}
				break;

				case LogType.Log:
				{
					return Level.Log;
				}
				break;

				case LogType.Exception:
				{
					return Level.Exception;
				}
				break;
			}

			return Level.None;
		}

		public static LogType ToUnityDebugLevel(this Level level)
		{
			switch (level)
			{
				case Level.None:
				{
					return LogType.Exception;
				}

				case Level.Exception:
				{
					return LogType.Exception;
				}

				case Level.Assert:
				{
					return LogType.Assert;
				}

				case Level.Error:
				{
					return LogType.Error;
				}

				case Level.Warning:
				{
					return LogType.Warning;
				}

				case Level.Log:
				{
					return LogType.Log;
				}
			}

			return LogType.Exception;
		}
	}
}
