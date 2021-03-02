using System.Collections.Generic;

namespace REF.Runtime.Notifications
{
	public class LocalNotification : Notification, ILocalNotification
	{
		private INotificationSettings settings;

		public INotificationSettings Settings { get { return settings; } set { settings = value; } }

		public LocalNotification(string title, string body, INotificationSettings settings) : base(title, body, null)
		{
			this.settings = settings;
		}

		public LocalNotification(string title, string body, INotificationSettings settings, IDictionary<string, string> data) : base(title, body, data)
		{
			this.settings = settings;
		}
	}
}
