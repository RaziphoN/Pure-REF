#if UNITY_IOS
using Unity.Notifications.iOS;

using System;
using System.Collections.Generic;

namespace REF.Runtime.Notifications.iOS
{
	public static class iOSNotificationHelper
	{
		public static iOSNotification ToPlatform(ILocalNotification notification)
		{
			IIosNotificationSettings settings = notification.Settings as IIosNotificationSettings;

			iOSNotification iosNotification = new iOSNotification()
			{
				Title = notification.Title,
				Body = notification.Body,
				Data = NotificationDataHelper.ToString(notification.Data),
				Subtitle = settings.Subtitle,

				ShowInForeground = settings.ShowInForeground,
				ForegroundPresentationOption = (PresentationOption)(int)settings.ForegroundPresentationOption,
				Badge = settings.Badge,
			};

			if (!string.IsNullOrEmpty(settings.Identifier))
				iosNotification.Identifier = settings.Identifier;

			var trigger = settings.Trigger;
			switch (trigger.Type)
			{
				case NotificationTrigger.TriggerType.Calendar:
				{
					var calendarTrigger = new iOSNotificationCalendarTrigger();
					var triggerTime = trigger.TriggerTime;

					calendarTrigger.Year = triggerTime.Year;
					calendarTrigger.Month = triggerTime.Month;
					calendarTrigger.Day = triggerTime.Day;
					calendarTrigger.Hour = triggerTime.Hour;
					calendarTrigger.Minute = triggerTime.Minute;
					calendarTrigger.Second = triggerTime.Second;

					iosNotification.Trigger = calendarTrigger;
				}
				break;

				case NotificationTrigger.TriggerType.TimeInterval:
				{
					var intervalTrigger = new iOSNotificationTimeIntervalTrigger();
					intervalTrigger.TimeInterval = trigger.Interval;
					intervalTrigger.Repeats = trigger.Repeat;

					iosNotification.Trigger = intervalTrigger;
				}
				break;

				case NotificationTrigger.TriggerType.ConcreteDayTime:
				{
					var intervalTrigger = new iOSNotificationTimeIntervalTrigger();
					intervalTrigger.TimeInterval = trigger.TriggerTime - DateTime.Now;
					intervalTrigger.Repeats = trigger.Repeat;

					iosNotification.Trigger = intervalTrigger;
				}
				break;

				default:
				break;
			}

			return iosNotification;
		}

		public static ILocalNotification ToLocal(iOSNotification notification)
		{
			var settings = GetSettings(notification);
			var title = notification.Title;
			var body = notification.Body;
			var data = NotificationDataHelper.FromString<Dictionary<string, string>>(notification.Data);

			return new LocalNotification(title, body, data, settings);
		}

		private static IIosNotificationSettings GetSettings(iOSNotification notification)
		{
			iOSNotificationSettingsWrapper settings = new iOSNotificationSettingsWrapper();

			settings.Identifier = notification.Identifier;
			settings.Subtitle = notification.Subtitle;
			settings.ShowInForeground = notification.ShowInForeground;
			settings.ForegroundPresentationOption = (iOSPresentationOption)(int)notification.ForegroundPresentationOption;
			settings.Badge = notification.Badge;

			// not working
			//if (notification.Trigger is iOSNotificationPushTrigger)
			//	settings.IsRemote = true;
			if (!LocalNotifications.IsLocal(settings.NotificationId))
				settings.IsRemote = true;

			return settings;
		}
	}
}
#endif