using UnityEngine;

using System.Diagnostics;

using REF.Runtime.Core;

namespace REF.Runtime.Diagnostic
{
	// NOTE: This class is strongly linked with library architecture.
	[CreateAssetMenu(fileName = "Logger", menuName = "REF/Diagnostic/Logger")]
	public class RefDebug : ServiceSO
	{
		private static RefDebug instance = null;

		private static RefDebug Instance 
		{ 
			get
			{
				if (instance == null)
				{
					var singletonName = typeof(RefDebug).Name;
					//Look for the singleton on the resources folder
					var assets = Resources.LoadAll<RefDebug>("");

					if (assets.Length == 0)
					{
						instance = CreateInstance<RefDebug>();
					}
					else
					{
						instance = assets[0];
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
	}
}
