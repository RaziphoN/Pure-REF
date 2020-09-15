namespace REF.Runtime.Diagnostic
{
	public interface ILogger
	{
		void Log(string tag, string format, UnityEngine.Object context = null, params object[] args);
		void Warning(string tag, string format, UnityEngine.Object context = null, params object[] args);
		void Error(string tag, string format, UnityEngine.Object context = null, params object[] args);
		void Exception(string tag, string format, UnityEngine.Object context = null, params object[] args);
		void Assert(bool condition, string tag, string format, UnityEngine.Object context = null, params object[] args);
	}
}
