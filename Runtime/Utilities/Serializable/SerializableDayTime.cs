using UnityEngine;

namespace REF.Runtime.Utilities.Serializable
{
	[System.Serializable]
	public struct SerializableDayTime
	{
		[Range(0, 59)] [SerializeField] private int seconds;
		[Range(0, 59)] [SerializeField] private int minutes;
		[Range(0, 23)] [SerializeField] private int hours;

		public int Seconds { get { return seconds; } set { seconds = Mathf.Clamp(value, 0, 59); } }
		public int Minutes { get { return minutes; } set { minutes = Mathf.Clamp(value, 0, 59); } }
		public int Hours { get { return hours; } set { hours = Mathf.Clamp(value, 0, 23); } }

		public SerializableDayTime(int hours, int minutes, int seconds)
		{
			this.hours = Mathf.Clamp(hours, 0, 23);
			this.minutes = Mathf.Clamp(minutes, 0, 59);
			this.seconds = Mathf.Clamp(seconds, 0, 59);
		}

		public bool IsZero()
		{
			return Seconds == 0 && Minutes == 0 && Hours == 0;
		}
	}
}
