﻿using REF.Runtime.Notifications;

namespace REF.Runtime.Online.Notifications
{
	public interface IPushNotificationService : IOnlineService
	{
		event System.Action<string> OnTokenReceived;
		event System.Action<INotification> OnNotificationReceived;

		string GetToken();
	}
}