#if REF_ONLINE_PUSH_NOTIFICATION

using REF.Runtime.Core;
using REF.Runtime.Notifications;

namespace REF.Runtime.Online.Notifications
{
	public interface IPushNotificationService : IOnlineService<IConfiguration>
	{
		event System.Action<string> OnTokenReceived;
		event System.Action<INotification> OnNotificationReceived;

		string GetToken();
	}
}

#endif
