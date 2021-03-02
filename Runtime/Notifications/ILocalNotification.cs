namespace REF.Runtime.Notifications
{
	public interface ILocalNotification : INotification
	{
		// This interface is a base for settings and if you use LocalNotifications class, Settings must implement IiOSNotificationSettings for iOS or IAndroidNotificationSettings for Android
		INotificationSettings Settings { get; }
	}
}
