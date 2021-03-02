using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace REF.Runtime.Notifications
{
	public static class NotificationDataHelper
	{
		public static IDictionary<string, string> FromString<T>(string data) where T : IDictionary<string, string>, new()
		{
			IDictionary<string, string> parsed = new T();

			if (!string.IsNullOrEmpty(data))
			{
				string[] properties = data.Trim(new char[] { '{', '}' }).Split(',');

				if (properties.Length > 0 && !string.IsNullOrEmpty(properties[0]))
				{
					for (int i = 0; i < properties.Length; ++i)
					{
						parsed.Add(StringToProperty(properties[i]));
					}
				}
			}

			return parsed;
		}

		public static string ToString(INotification notification)
		{
			StringBuilder builder = new StringBuilder();

			var keys = notification.GetKeys();

			if (keys.Count() > 0)
			{
				builder.Append("{");

				foreach (var key in keys)
				{
					var value = notification.Get(key);

					if (builder.Length > 2)
						builder.Append(',');

					StringifyProperty(builder, new KeyValuePair<string, string>(key, value));
				}

				builder.Append("}");
			}

			return builder.ToString();
		}

		private static void StringifyProperty(StringBuilder builder, KeyValuePair<string, string> pair)
		{
			builder.Append('\"').Append(pair.Key).Append('\"').Append(':').Append('\"').Append(pair.Value).Append('\"');
		}

		private static KeyValuePair<string, string> StringToProperty(string data)
		{
			string[] values = data.Split(':');
			return new KeyValuePair<string, string>(values[0].Trim('\"'), values[1].Trim('\"'));
		}
	}
}
