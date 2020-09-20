using System.Diagnostics;

namespace REF.Runtime.Diagnostic
{
	public static class ObjectLogExtension
	{
		[Conditional("REF_LOG_VERBOSE")]
		public static void Log(this UnityEngine.Object context, string format, params object[] args)
		{
			RefDebug.Log(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public static void LogWarning(this UnityEngine.Object context, string format, params object[] args)
		{
			RefDebug.Warning(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public static void LogError(this UnityEngine.Object context, string format, params object[] args)
		{
			RefDebug.Error(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public static void LogAssert(this UnityEngine.Object context, bool condition, string format, params object[] args)
		{
			RefDebug.Assert(condition, context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public static void LogException(this UnityEngine.Object context, string format, params object[] args)
		{
			RefDebug.Exception(context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE")]
		public static void Log(this UnityEngine.Object context, UnityEngine.Color color, string format, params object[] args)
		{
			RefDebug.Log(color, context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public static void LogWarning(this UnityEngine.Object context, UnityEngine.Color color, string format, params object[] args)
		{
			RefDebug.Warning(color, context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public static void LogError(this UnityEngine.Object context, UnityEngine.Color color, string format, params object[] args)
		{
			RefDebug.Error(color, context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public static void LogAssert(this UnityEngine.Object context, bool condition, UnityEngine.Color color, string format, params object[] args)
		{
			RefDebug.Assert(condition, color, context.GetType().Name, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public static void LogException(this UnityEngine.Object context, UnityEngine.Color color, string format, params object[] args)
		{
			RefDebug.Exception(color, context.GetType().Name, format, context, args);
		}
	}
}
