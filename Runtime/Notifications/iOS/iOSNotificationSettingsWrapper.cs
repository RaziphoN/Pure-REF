namespace REF.Runtime.Notifications.iOS
{
	public class iOSNotificationSettingsWrapper : IIosNotificationSettings
	{
		public NotificationId NotificationId { get { return new NotificationId(Identifier); } }
		public bool IsRemote { get; set; } = false;
		public string Identifier { get; set; } = string.Empty;
		public string Subtitle { get; set; }
		public bool ShowInForeground { get; set; }
		public iOSPresentationOption ForegroundPresentationOption { get; set; } = iOSPresentationOption.Alert;
		public int Badge { get; set; } = -1;
		public NotificationTrigger Trigger { get; }
	}
}
