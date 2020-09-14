using UnityEngine;

using System;


namespace REF.Runtime.Notifications.Android
{
	public class AndroidNotificationSettingsWrapper : IAndroidNotificationSettings
	{
		public NotificationId NotificationId { get { return new NotificationId(Id); } }
		public bool IsRemote { get; set; }

		public int Id { get; set; }
		public string ChannelId { get; set; }

		public AndroidGroupAlertBehaviours GroupAlertBehaviour { get; set; }
		public bool GroupSummary { get; set; }
		public string Group { get; set; }

		public bool UsesStopwatch { get; set; }
		public bool ShouldAutoCancel { get; set; }
		public int Number { get; set; }
		public string SortKey { get; set; }

		public Color? Color { get; set; }
		public AndroidNotificationStyle Style { get; set; }

		public string SmallIcon { get; set; }
		public string LargeIcon { get; set; }

		public TimeSpan? RepeatInterval { get; set; }
		public DateTime FireTime { get; set; }
		public NotificationTrigger Trigger { get; }
	}
}
