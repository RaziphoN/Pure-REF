#if REF_ONLINE_PUSH_NOTIFICATION

using REF.Runtime.Notifications;

namespace REF.Runtime.Online.Notifications
{
	public interface IPushNotificationService : IOnlineService
	{
		event System.Action<string> OnTokenReceived;
		event System.Action<INotification> OnNotificationReceived;

		void Subscribe(string topic);
		void Unsubscribe(string topic);

		string GetToken();
	}
}

#endif
