﻿using UnityEngine;

using System.Linq;
using System.Diagnostics;

namespace REF.Runtime.Diagnostic
{
	public class Debug
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

		public enum Mode
		{
			Exclusive,
			Inclusive,
			Ignore,
		}

		[SerializeField] private Level level = Level.Log;
		[SerializeField] private Mode mode = Mode.Ignore;
		[SerializeField] private string[] tags;
		[SerializeField] private bool stacktrace = true;
		[SerializeField] private Color color = Color.white;

		public Color GetColor()
		{
			return color;
		}

		public void SetColor(Color color)
		{
			this.color = color;
		}

		[Conditional("REF_LOG_VERBOSE")]
		public void Log(string tag, string format, params object[] args)
		{
			Internal(Level.Log, tag, format, null, args);
		}

		[Conditional("REF_LOG_VERBOSE")]
		public void Log(string tag, string format, Object context, params object[] args)
		{
			Internal(Level.Log, tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public void Warning(string tag, string format, params object[] args)
		{
			Internal(Level.Warning, tag, format, null, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public void Warning(string tag, string format, Object context, params object[] args)
		{
			Internal(Level.Warning, tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public void Error(string tag, string format, params object[] args)
		{
			Internal(Level.Error, tag, format, null, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public void Error(string tag, string format, Object context, params object[] args)
		{
			Internal(Level.Error, tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public void Exception(string tag, string format, params object[] args)
		{
			Internal(Level.Exception, tag, format, null, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public void Exception(string tag, string format, Object context, params object[] args)
		{
			Internal(Level.Exception, tag, format, context, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public void Assert(bool condition, string tag, string format, params object[] args)
		{
			if (!condition)
			{
				Internal(Level.Assert, tag, format, null, args);
			}
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public void Assert(bool condition, string tag, string format, Object context, params object[] args)
		{
			if (!condition)
			{
				Internal(Level.Assert, tag, format, context, args);
			}
		}

		private void Internal(Level level, string tag, string format, Object context, params object[] args)
		{
			if (this.level < level || this.level == Level.None)
			{
				return;
			}

			if (mode != Mode.Ignore)
			{
				bool hasTag = tags.Contains(tag);
				if (mode == Mode.Inclusive && !hasTag)
				{
					return;
				}

				if (mode == Mode.Exclusive && hasTag)
				{
					return;
				}
			}

			var formatted = string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), format);
			UnityEngine.Debug.LogFormat(ConvertLevel(this.level), stacktrace ? LogOption.None : LogOption.NoStacktrace, context, formatted, args);
		}

		private LogType ConvertLevel(Level level)
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