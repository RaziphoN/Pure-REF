using UnityEngine;

using System;

using REF.Runtime.Utilities.Serializable;

namespace REF.Runtime.Notifications
{
	[Serializable]
	public class NotificationTrigger
	{
		public enum TriggerType
		{
			Calendar,
			TimeInterval,
			ConcreteDayTime
		}

		[SerializeField] private TriggerType type;
		[SerializeField] private SerializableTimeSpan interval;
		[SerializeField] private SerializableDateTime time;
		[SerializeField] private bool repeat;

		public TriggerType Type { get { return type; } set { type = value; } }
		public bool Repeat { get { return repeat; } set { repeat = value; } }

		public TimeSpan Interval { get { return interval.Span; } set { interval.Span = value; } }
		public DateTime Time { get { return time.DateTime; } set { time.DateTime = value; } }

		public DateTime TriggerTime
		{
			get
			{
				switch (Type)
				{
					case TriggerType.Calendar:
						return time.DateTime;

					case TriggerType.TimeInterval:
						return DateTime.Now + interval.Span;

					case TriggerType.ConcreteDayTime:
					{
						var triggerTime = DateTime.Now;
						triggerTime.AddDays(interval.Days);

						if (triggerTime.TimeOfDay > interval.Span)
							triggerTime = triggerTime.Subtract(triggerTime.TimeOfDay - interval.Span);
						else
							triggerTime = triggerTime.Add(interval.Span - triggerTime.TimeOfDay);

						return triggerTime;
					}

					default:
						return DateTime.Now + new TimeSpan(0, 1, 0);
				}
			}
		}

		public void SetCanlendarTrigger(DateTime triggerTime)
		{
			type = TriggerType.Calendar;
			time.DateTime = triggerTime;
		}

		public void SetIntervalTrigger(TimeSpan interval)
		{
			type = TriggerType.TimeInterval;
			this.interval.Span = interval;
		}

		// use days on time span to determine interval from now to trigger notification at hours:minutes:seconds of a given day
		public void SetConcreteDayTimeTrigger(TimeSpan daysTime)
		{
			type = TriggerType.ConcreteDayTime;
			interval.Span = daysTime;
		}
	}
}
