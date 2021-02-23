using UnityEngine;

using System.Diagnostics;

using REF.Runtime.Online;
using REF.Runtime.Core;

#if REF_ONLINE_CRASH_REPORT
	using REF.Runtime.Online.CrashReports;
#endif

namespace REF.Runtime.Diagnostic
{
	[System.Serializable]
	public class RefDebug : ServiceBase, IOnlineService
	{
		private static RefDebug instance = null;

		private static RefDebug Instance 
		{
			get
			{
				if (instance == null)
				{
					UnityEngine.Debug.LogError("No Instance of RefDebug");
					instance = new RefDebug();
				}

				return instance;
			}
		}

		[SerializeField] private CloudDebug cloudLogger = new CloudDebug();
		[SerializeField] private Debug logger = new Debug();

		public static Color GetColor()
		{
			return Instance.logger.GetColor();
		}

		public static void SetColor(Color color)
		{
			Instance.logger.SetColor(color);
		}

		[Conditional("REF_LOG_VERBOSE")]
		public static void Log(string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Log(tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE")]
		public static void Log(Color color, string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Log(color, tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public static void Warning(string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Warning(tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public static void Warning(Color color, string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Warning(color, tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public static void Error(string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Error(tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public static void Error(Color color, string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Error(color, tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public static void Exception(string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Exception(tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public static void Exception(Color color, string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Exception(color, tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public static void Assert(bool condition, string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Assert(condition, tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public static void Assert(bool condition, Color color, string tag, string format, Object context = null, params object[] args)
		{
			Instance.logger.Assert(condition, color, tag, format, context, args);
		}

		public RefDebug()
		{
			instance = this;
		}

		public override void Construct(IApp app)
		{
			base.Construct(app);

#if REF_ONLINE_CRASH_REPORT
			var cloudDebugService = app.Get<ICloudDebugService>();
			cloudLogger.Construct(cloudDebugService);
#endif
		}

		public override void PreInitialize(System.Action callback)
		{
#if REF_ONLINE_CRASH_REPORT
			cloudLogger.Initialize();
#endif
			base.PreInitialize(callback);
		}

		public override void Release(System.Action callback)
		{
#if REF_ONLINE_CRASH_REPORT
			cloudLogger.Release();
#endif
			base.Release(callback);
		}
	}
}
