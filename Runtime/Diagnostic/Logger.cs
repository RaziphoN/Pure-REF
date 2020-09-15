using UnityEngine;

using System.Diagnostics;

using REF.Runtime.Core;

namespace REF.Runtime.Diagnostic
{
	// NOTE: This class is strongly linked with library architecture.
	[CreateAssetMenu(fileName = "Logger", menuName = "REF/Diagnostic/Logger")]
	public class Logger : ServiceBase
	{
		private static Logger instance = null;

		private static Logger Instance 
		{ 
			get
			{
				if (instance == null)
				{
					var loggerObjects = Resources.FindObjectsOfTypeAll<Logger>();
					
					if (loggerObjects != null && loggerObjects.Length > 0)
					{
						instance = loggerObjects[0];
					}
					else
					{
						instance = CreateInstance<Logger>();
					}
				}

				return instance;
			}
		}

		[SerializeField] private Debug logger = new Debug();

		public override bool IsInitialized()
		{
			return true;
		}

		[Conditional("REF_LOG_VERBOSE")]
		public static void Log(string tag, string format, params object[] args)
		{
			Instance.logger.Log(tag, format, null, args);
		}

		[Conditional("REF_LOG_VERBOSE")]
		public static void Log(string tag, string format, Object context, params object[] args)
		{
			Instance.logger.Log(tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public static void Warning(string tag, string format, params object[] args)
		{
			Instance.logger.Warning(tag, format, null, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public static void Warning(string tag, string format, Object context, params object[] args)
		{
			Instance.logger.Warning(tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public static void Error(string tag, string format, params object[] args)
		{
			Instance.logger.Error(tag, format, null, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public static void Error(string tag, string format, Object context, params object[] args)
		{
			Instance.logger.Error(tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public static void Exception(string tag, string format, params object[] args)
		{
			Instance.logger.Exception(tag, format, null, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public static void Exception(string tag, string format, Object context, params object[] args)
		{
			Instance.logger.Warning(tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public static void Assert(bool condition, string tag, string format, params object[] args)
		{
			Instance.logger.Assert(condition, tag, format, null, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public static void Assert(bool condition, string tag, string format, Object context, params object[] args)
		{
			Instance.logger.Assert(condition, tag, format, context, args);
		}
	}

	public static class ObjectLogExtension
	{
		[Conditional("REF_LOG_VERBOSE")]
		public static void Log(this Object context, string format, params object[] args)
		{
			Logger.Log(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public static void LogWarning(this Object context, string format, params object[] args)
		{
			Logger.Warning(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public static void LogError(this Object context, string format, params object[] args)
		{
			Logger.Error(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public static void LogAssert(this Object context, bool condition, string format, params object[] args)
		{
			Logger.Assert(condition, context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public static void LogException(this Object context, string format, params object[] args)
		{
			Logger.Exception(context.GetType().Name, format, context, args);
		}
	}
}
