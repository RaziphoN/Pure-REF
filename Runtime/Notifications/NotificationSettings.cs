using UnityEngine;

using REF.Runtime.Notifications.Android;
using REF.Runtime.Notifications.iOS;

namespace REF.Runtime.Notifications
{
	public class NotificationSettings : IAndroidNotificationSettings, IIosNotificationSettings
	{
		public int Id { get; set; } = -1;
		public string ChannelId { get; set; } = "DefaultGameChannel";
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

		public string Identifier { get; set; }
		public string Subtitle { get; set; }
		public bool ShowInForeground { get; set; }
		public iOSPresentationOption ForegroundPresentationOption { get; set; }
		public int Badge { get; set; }

		public bool IsRemote { get; } = false;
		public NotificationTrigger Trigger { get; } = new NotificationTrigger();
		public NotificationId NotificationId
		{
			get
			{
#if UNITY_ANDROID
				return new NotificationId(Id);
#elif UNITY_IOS
				return new NotificationId(Identifier);
#endif
				return new NotificationId();
			}
		}
	}
}
