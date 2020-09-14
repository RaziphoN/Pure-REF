using UnityEngine;

namespace REF.Runtime.Utilities.Serializable
{
	[System.Serializable]
	public struct SerializableDate
	{
		[Range(1, 31)] [SerializeField] private int day;
		[Range(1, 12)] [SerializeField] private int month;
		[SerializeField] private int year;

		public int Day { get { return day; } set { day = Mathf.Clamp(value, 1, 31); } }
		public int Month { get { return month; } set { month = Mathf.Clamp(value, 1, 12); } }
		public int Year { get { return year; } set { year = value; } }
	}
}
