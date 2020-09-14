namespace REF.Runtime.Notifications.iOS
{
	public enum iOSPresentationOption
	{
		None = 0,
		Badge = 1,
		Sound = 2,
		Alert = 4,
		BadgeSound = Badge | Sound,
		BadgeAlert = Badge | Alert,
		SoundAlert = Sound | Alert,
		BadgeSoundAlert = Sound | Badge | Alert
	}

	public interface IIosNotificationSettings : INotificationSettings
	{
		string Identifier { get; set; }
		string Subtitle { get; set; }
		bool ShowInForeground { get; set; }
		iOSPresentationOption ForegroundPresentationOption { get; set; }
		int Badge { get; set; }
	}
}
