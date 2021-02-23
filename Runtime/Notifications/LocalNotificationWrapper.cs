using System;
using System.Collections.Generic;

#if UNITY_ANDROID
using Unity.Notifications.Android;
using REF.Runtime.Notifications.Android;
#endif

#if UNITY_IOS
using Unity.Notifications.iOS;
using REF.Runtime.Notifications.iOS;
#endif

namespace REF.Runtime.Notifications
{
	public static class LocalNotificationWrapper
	{
		private static event Action<ILocalNotification> OnNotificationReceived;
		private static List<NotificationId> localNotificationIds = new List<NotificationId>();

#if UNITY_ANDROID
		private static List<string> registeredAndroidChannels = new List<string>();
#endif

		public static bool IsLocal(NotificationId id)
		{
			var found = localNotificationIds.Find(localId =>
			{
				return localId.Equals(id);
			});

			return found != null;
		}

		public static void Initialize()
		{
#if UNITY_ANDROID
			AndroidNotificationCenter.Initialize();
			AndroidNotificationCenter.OnNotificationReceived += OnAndroidNotificationReceived;
#elif UNITY_IOS
			var scheduled = iOSNotificationCenter.GetScheduledNotifications();

			if (scheduled != null)
			{
				for (int idx = 0; idx < scheduled.Length; ++idx)
					localNotificationIds.Add(new NotificationId(scheduled[idx].Identifier));
			}

			iOSNotificationCenter.OnNotificationReceived += OniOSNotificationReceived;
#endif
		}

		public static void Release()
		{
#if UNITY_ANDROID
			AndroidNotificationCenter.OnNotificationReceived -= OnAndroidNotificationReceived;
#elif UNITY_IOS
			iOSNotificationCenter.OnNotificationReceived -= OniOSNotificationReceived;
#endif
		}

		public static void Subscribe(Action<ILocalNotification> OnNotificationReceived)
		{
			LocalNotificationWrapper.OnNotificationReceived += OnNotificationReceived;
		}

		public static void Unsubscribe(Action<ILocalNotification> OnNotificationReceived)
		{
			LocalNotificationWrapper.OnNotificationReceived -= OnNotificationReceived;
		}

		public static NotificationId Schedule(ILocalNotification notification)
		{
			if (notification.Settings.Trigger.TriggerTime <= DateTime.Now)
				return new NotificationId();

#if UNITY_IOS
			var settings = notification.Settings as IIosNotificationSettings;
			if (settings == null)
				return new NotificationId();

			var platformNotification = iOSNotificationHelper.ToPlatform(notification);
			iOSNotificationCenter.ScheduleNotification(platformNotification);


			var id = new NotificationId(platformNotification.Identifier);
			localNotificationIds.Add(id);
			return id;

#elif UNITY_ANDROID

#if UNITY_EDITOR
			// imitate notification ids in editor
			var id = new NotificationId(Guid.NewGuid().GetHashCode());
			localNotificationIds.Add(id);
			return id;
#else
			var settings = notification.Settings as IAndroidNotificationSettings;
			if (settings == null)
				return new NotificationId();

			var platformNotification = AndroidNotificationHelper.ToPlatform(notification);

			if (!registeredAndroidChannels.Contains(settings.ChannelId))
			{
				AndroidNotificationCenter.RegisterNotificationChannel(new AndroidNotificationChannel(settings.ChannelId, "Custom Channel", "Custom Channel Description", Importance.Default));
				registeredAndroidChannels.Add(settings.ChannelId);
			}

			if (settings.Id != -1)
			{
				AndroidNotificationCenter.SendNotificationWithExplicitID(platformNotification, settings.ChannelId, settings.Id);
				var id = new NotificationId(settings.Id);
				localNotificationIds.Add(id);

				return id;
			}
			else
			{
				var id = new NotificationId(AndroidNotificationCenter.SendNotification(platformNotification, settings.ChannelId));
				localNotificationIds.Add(id);

				return id;
			}
#endif // UNITY_EDITOR
#endif // UNITY_ANDROID
			return new NotificationId();
		}

		public static void CancelScheduled(NotificationId id)
		{
			if (!id.IsValid())
				return;

#if UNITY_IOS
			iOSNotificationCenter.RemoveScheduledNotification(id.iOSId);
#elif UNITY_ANDROID
			AndroidNotificationCenter.CancelScheduledNotification(id.AndroidId);
#endif
		}

		public static void CancelAllScheduled()
		{
#if UNITY_IOS
			iOSNotificationCenter.RemoveAllScheduledNotifications();
#elif UNITY_ANDROID
			AndroidNotificationCenter.CancelAllScheduledNotifications();
#endif
		}

		public static void CancelDisplayed(NotificationId id)
		{
			if (!id.IsValid())
				return;

#if UNITY_IOS
			iOSNotificationCenter.RemoveDeliveredNotification(id.iOSId);
#elif UNITY_ANDROID
			AndroidNotificationCenter.CancelDisplayedNotification(id.AndroidId);
#endif
		}

		public static void CancelAllDisplayed()
		{
#if UNITY_IOS
			iOSNotificationCenter.RemoveAllDeliveredNotifications();
#elif UNITY_ANDROID
			AndroidNotificationCenter.CancelAllDisplayedNotifications();
#endif
		}

		public static void CancelAll()
		{
#if UNITY_IOS
			iOSNotificationCenter.RemoveAllScheduledNotifications();
			iOSNotificationCenter.RemoveAllDeliveredNotifications();
#elif UNITY_ANDROID
			AndroidNotificationCenter.CancelAllNotifications();
#endif
		}

		public static ILocalNotification GetLastNotification()
		{
#if UNITY_IOS
			var notification = iOSNotificationCenter.GetLastRespondedNotification();
			if (notification != null)
				return iOSNotificationHelper.ToLocal(notification);
			else
				return null;
#elif UNITY_ANDROID
			var notificationIntent = AndroidNotificationCenter.GetLastNotificationIntent();
			if (notificationIntent != null)
				return AndroidNotificationHelper.ToLocal(notificationIntent);
			else
				return null;
#endif

			return null;
		}

#if UNITY_ANDROID
		private static void OnAndroidNotificationReceived(AndroidNotificationIntentData data)
		{
			var localNotification = AndroidNotificationHelper.ToLocal(data);
			OnNotificationReceived?.Invoke(localNotification);

			var id = localNotification.Settings.NotificationId;
			if (IsLocal(id))
				localNotificationIds.Remove(id);
		}
#endif

#if UNITY_IOS
		private static void OniOSNotificationReceived(iOSNotification notification)
		{
			var localNotification = iOSNotificationHelper.ToLocal(notification);
			OnNotificationReceived?.Invoke(localNotification);

			var id = localNotification.Settings.NotificationId;
			if (IsLocal(id))
				localNotificationIds.Remove(id);
		}
#endif
	}
}
