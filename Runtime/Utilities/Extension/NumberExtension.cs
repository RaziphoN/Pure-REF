namespace REF.Runtime.Utilities.Extension
{
	public static class NumberExtension
	{
		public static int SafeAdd(this int data, int val)
		{
			if (int.MaxValue - data < val)
			{
				return int.MaxValue;
			}
			else
			{
				return data + val;
			}
		}

		public static long SafeAdd(this long data, long val)
		{
			if (long.MaxValue - data < val)
			{
				return long.MaxValue;
			}
			else
			{
				return data + val;
			}
		}
	}
}
