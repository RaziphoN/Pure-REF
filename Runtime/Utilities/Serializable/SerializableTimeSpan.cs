using UnityEngine;

using System;

namespace REF.Runtime.Utilities.Serializable
{
	[Serializable]
	public struct SerializableTimeSpan
	{
		[SerializeField] private SerializableDayTime time;
		[SerializeField] private int days;

		public int Seconds { get { return time.Seconds; } set { time.Seconds = value; } }
		public int Minutes { get { return time.Minutes; } set { time.Minutes = value; } }
		public int Hours { get { return time.Hours; } set { time.Hours = value; } }
		public int Days { get { return days; } set { days = value; } }

		public SerializableTimeSpan(TimeSpan span)
		{
			days = span.Days;
			time = new SerializableDayTime(span.Hours, span.Minutes, span.Seconds);
		}

		public TimeSpan Span
		{
			get
			{
				return new TimeSpan(days, time.Hours, time.Minutes, time.Seconds);
			}

			set
			{
				days = value.Days;

				time.Hours = value.Hours;
				time.Minutes = value.Minutes;
				time.Seconds = value.Seconds;
			}
		}

		public bool IsZero()
		{
			return time.IsZero() && Days == 0;
		}
	}
}
