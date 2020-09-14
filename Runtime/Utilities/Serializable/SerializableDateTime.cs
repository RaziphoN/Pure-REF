using UnityEngine;

using System;

namespace REF.Runtime.Utilities.Serializable
{
	[System.Serializable]
	public struct SerializableDateTime
	{
		[SerializeField] private DateTimeKind kind;
		[SerializeField] private SerializableDate date;
		[SerializeField] private SerializableDayTime time;

		public DateTime DateTime
		{
			get
			{
				return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds, kind);
			}

			set
			{
				kind = value.Kind;

				date.Year = value.Year;
				date.Month = value.Month;
				date.Day = value.Day;

				time.Hours = value.Hour;
				time.Minutes = value.Minute;
				time.Seconds = value.Second;
			}
		}
	}
}
