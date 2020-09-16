using System.Diagnostics;

namespace REF.Runtime.Diagnostic
{
	public static class ObjectLogExtension
	{
		[Conditional("REF_LOG_VERBOSE")]
		public static void Log(this UnityEngine.Object context, string format, params object[] args)
		{
			Logger.Log(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public static void LogWarning(this UnityEngine.Object context, string format, params object[] args)
		{
			Logger.Warning(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public static void LogError(this UnityEngine.Object context, string format, params object[] args)
		{
			Logger.Error(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public static void LogAssert(this UnityEngine.Object context, bool condition, string format, params object[] args)
		{
			Logger.Assert(condition, context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public static void LogException(this UnityEngine.Object context, string format, params object[] args)
		{
			Logger.Exception(context.GetType().Name, format, context, args);
		}
	}
}
