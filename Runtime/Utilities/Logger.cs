using UnityEngine;

using System.Threading;

namespace Scripts.Framework.Utils
{
	public static class FLogger
	{
		public static void Assert(UnityEngine.Object obj, bool condition, object message)
		{
			Debug.Assert(condition, GetDebugInfoPrefix(obj) + message, obj);
		}

		public static void Log(UnityEngine.Object obj, object message)
		{
			Debug.Log(GetDebugInfoPrefix(obj) + message, obj);
		}

		public static void LogFormat(UnityEngine.Object obj, string format, params object[] args)
		{
			format = GetDebugInfoPrefix(obj) + format;
			Debug.LogFormat(obj, format, args);
		}

		public static void LogWarning(UnityEngine.Object obj, object message)
		{
			Debug.LogWarning(GetDebugInfoPrefix(obj) + message, obj);
		}

		public static void LogError(UnityEngine.Object obj, object message)
		{
			Debug.LogError(GetDebugInfoPrefix(obj) + message, obj);
		}

		public static void Assert(System.Type obj, bool condition, object message)
		{
			Debug.Assert(condition, GetDebugInfoPrefix(obj) + message);
		}

		public static void Log(System.Type obj, object message)
		{
			Debug.Log(GetDebugInfoPrefix(obj) + message);
		}

		public static void LogFormat(System.Type obj, string format, params object[] args)
		{
			format = GetDebugInfoPrefix(obj) + format;
			Debug.LogFormat(format, args);
		}

		public static void LogWarning(System.Type obj, object message)
		{
			Debug.LogWarning(GetDebugInfoPrefix(obj) + message);
		}

		public static void LogError(System.Type obj, object message)
		{
			Debug.LogError(GetDebugInfoPrefix(obj) + message);
		}

		private static string GetDebugInfoPrefix(System.Type type)
		{
			return $"[{type.Name}] - ";
		}

		private static string GetDebugInfoPrefix(Object obj)
		{
			return $"[{obj.GetType().Name}][{obj.name}][{Time.time}][{Thread.CurrentThread.ManagedThreadId}] -";
		}
	}
}
