using REF.Runtime.Core;

namespace REF.Runtime.Notifications
{
	public interface ILocalNotificationService : IService
	{
		event System.Action<ILocalNotification> OnLocalNotificationReceived;

		bool IsScheduled(NotificationId id);

		ILocalNotification GetLastNotification();

		NotificationId Schedule(ILocalNotification notification);

		void CancelDisplayed(NotificationId id);
		void CancelScheduled(NotificationId id);

		void CancelAllDisplayed();
		void CancelAllScheduled();
		void CancelAll();
	}
}