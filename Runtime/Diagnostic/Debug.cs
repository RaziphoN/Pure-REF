﻿using UnityEngine;

using System.Linq;
using System.Diagnostics;

namespace REF.Runtime.Diagnostic
{
	[System.Serializable]
	public class Debug
	{
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
		public void Log(string tag, string format, Object context = null, params object[] args)
		{
			Internal(Level.Log, tag, format, context, color, args);
		}

		[Conditional("REF_LOG_VERBOSE")]
		public void Log(Color color, string tag, string format, Object context = null, params object[] args)
		{
			Internal(Level.Log, tag, format, context, color, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public void Warning(string tag, string format, Object context = null, params object[] args)
		{
			Internal(Level.Warning, tag, format, context, color, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING")]
		public void Warning(Color color, string tag, string format, Object context = null, params object[] args)
		{
			Internal(Level.Warning, tag, format, context, color, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public void Error(string tag, string format, Object context = null, params object[] args)
		{
			Internal(Level.Error, tag, format, context, Color.red, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR")]
		public void Error(Color color, string tag, string format, Object context = null, params object[] args)
		{
			Internal(Level.Error, tag, format, context, color, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public void Exception(string tag, string format, Object context = null, params object[] args)
		{
			Internal(Level.Exception, tag, format, context, Color.red, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION")]
		public void Exception(Color color, string tag, string format, Object context = null, params object[] args)
		{
			Internal(Level.Exception, tag, format, context, color, args);
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public void Assert(bool condition, string tag, string format, Object context = null, params object[] args)
		{
			if (!condition)
			{
				Internal(Level.Assert, tag, format, context, color, args);
			}
		}

		[Conditional("REF_LOG_VERBOSE"), Conditional("REF_LOG_WARNING"), Conditional("REF_LOG_ERROR"), Conditional("REF_LOG_EXCEPTION"), Conditional("REF_LOG_ASSERT")]
		public void Assert(bool condition, Color color, string tag, string format, Object context = null, params object[] args)
		{
			if (!condition)
			{
				Internal(Level.Assert, tag, format, context, color, args);
			}
		}

		private void Internal(Level level, string tag, string format, Object context, Color color, params object[] args)
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

#if UNITY_EDITOR
			var formatted = string.Format("<color=#{0:X2}{1:X2}{2:X2}>[{3}] - {4}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), tag, format);
#else
			var formatted = string.Format("[{3}] - {4}", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), tag, format);
#endif

			UnityEngine.Debug.LogFormat(this.level.ToUnityDebugLevel(), stacktrace ? LogOption.None : LogOption.NoStacktrace, context, formatted, args);
		}
	}
}