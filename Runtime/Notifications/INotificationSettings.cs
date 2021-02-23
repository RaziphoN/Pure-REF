namespace REF.Runtime.Notifications
{
	public interface INotificationSettings
	{
		NotificationId NotificationId { get; }
		bool IsRemote { get; }
		NotificationTrigger Trigger { get; }
	}
}
