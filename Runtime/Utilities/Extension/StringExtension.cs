using System;
using System.Linq;


namespace REF.Runtime.Utilities.Extension
{
    public static class StringExtension
    {
        public static string Remove(this String str, params char[] ch)
        {
            var s = string.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                if (!ch.Contains(str[i]))
                {
                    s += str[i];
                }
            }
           return s;
        }

		public static string TrimToLength(this String str, int length)
		{
			if (length > 0)
				return str.Length > length ? str.Substring(0, length) : str;

			return str;
		}
    }
}
