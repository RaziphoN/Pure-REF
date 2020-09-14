using UnityEngine;

using REF.Runtime.Notifications.iOS;

namespace REF.Runtime.Notifications
{
	[System.Serializable]
	public class SerializableIOSNotificationSettings : IIosNotificationSettings
	{
		[SerializeField] private string identifier = string.Empty;
		[SerializeField] private string subtitle = string.Empty;

		[Space]

		// Visualization
		[SerializeField] private int badge = -1;
		[SerializeField] private bool showInForeground = false;
		[SerializeField] private iOSPresentationOption presentationOption = iOSPresentationOption.Alert;

		[Space]

		[SerializeField] private NotificationTrigger trigger = new NotificationTrigger();

		public NotificationId NotificationId { get { return new NotificationId(Identifier); } }
		public bool IsRemote { get; } = false;
		public string Identifier { get { return identifier; } set { identifier = value; } }
		public string Subtitle { get { return subtitle; } set { subtitle = value; } }
		public bool ShowInForeground { get { return showInForeground; } set { showInForeground = value; } }
		public iOSPresentationOption ForegroundPresentationOption { get { return presentationOption; } set { presentationOption = value; } }
		public int Badge { get { return badge; } set { badge = value; } }
		NotificationTrigger INotificationSettings.Trigger { get { return trigger; } }
	}
}
