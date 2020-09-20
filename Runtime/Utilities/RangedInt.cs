using UnityEngine;

namespace REF.Runtime.Utilities
{
	[System.Serializable]
	public class RangedInt
	{
		public RangedInt(int start, int length)
		{
			Start = start;
			Length = Mathf.Max(1, length);
		}

		public int Start = 0;
		[Min(1)] public int Length = 1;

		public int End
		{
			get
			{
				return Start + Length;
			}
		}
	}
}
