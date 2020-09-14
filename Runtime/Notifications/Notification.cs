using System.Collections.Generic;

namespace REF.Runtime.Notifications
{
	public class Notification : INotification
	{
		public string Title { get; set; }
		public string Body { get; set; }
		public IDictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

		public Notification(string title, string body, IDictionary<string, string> data = null)
		{
			Title = title;
			Body = body;

			if (data != null)
			{
				foreach (var record in data)
				{
					Data[record.Key] = record.Value;
				}
			}
		}

		public override string ToString()
		{
			string result = "{Notification} [Title]: " + Title + ", [Body]: " + Body;

			if (Data != null)
			{
				foreach (var record in Data)
				{
					result += ", [" + record.Key + "]: " + record.Value;
				}
			}

			return result;
		}
	}
}
