using UnityEngine;

namespace REF.Runtime.Notifications.iOS
{
	[System.Serializable]
	public class iOSNotificationSettingsWrapper : IIosNotificationSettings
	{
		[SerializeField] private string id = string.Empty;
		[SerializeField] private bool isRemote;

		[SerializeField] private string subtitle;

		[SerializeField] private iOSPresentationOption foregroundPresentation = iOSPresentationOption.Alert;
		[SerializeField] private bool showInForeground;

		[SerializeField, Min(-1)] private int badge = -1;
		[SerializeField] private NotificationTrigger trigger = new NotificationTrigger();

		public bool IsRemote { get { return isRemote; } internal set { isRemote = value; } }

		public NotificationId NotificationId { get { return new NotificationId(Identifier); } }
		public string Identifier { get { return id; } set { id = value; } }

		public string Subtitle { get { return subtitle; } set { subtitle = value; } }

		public iOSPresentationOption ForegroundPresentationOption { get { return foregroundPresentation; } set { foregroundPresentation = value; } }
		public bool ShowInForeground { get { return showInForeground; } set { showInForeground = value; } }
		public int Badge { get { return badge; } set { badge = value; } }
		public NotificationTrigger Trigger { get { return trigger; } set { trigger = value; } }
	}
}
