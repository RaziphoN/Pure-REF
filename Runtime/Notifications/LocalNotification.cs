using System.Collections.Generic;

namespace REF.Runtime.Notifications
{
	public class LocalNotification : ILocalNotification
	{
		public string Title { get; set; }
		public string Body { get; set; }
		public IDictionary<string, string> Data { get; set; } = new Dictionary<string, string>();
		public INotificationSettings Settings { get; set; }

		public LocalNotification(string title, string body, IDictionary<string, string> data, INotificationSettings settings)
		{
			Title = title;
			Body = body;
			Data = data;

			Settings = settings;
		}
	}
}
