#if UNITY_ANDROID
using Unity.Notifications.Android;

using System.Collections.Generic;


namespace REF.Runtime.Notifications.Android
{
	public static class AndroidNotificationHelper
	{
		public static AndroidNotification ToPlatform(ILocalNotification notification)
		{
			var settings = notification.Settings as IAndroidNotificationSettings;

			AndroidNotification androidNotification = new AndroidNotification()
			{
				Title = notification.Title,
				Text = notification.Body,
				IntentData = NotificationDataHelper.ToString(notification),

				GroupAlertBehaviour = (GroupAlertBehaviours)(int)settings.GroupAlertBehaviour,
				GroupSummary = settings.GroupSummary,
				Group = settings.Group,

				UsesStopwatch = settings.UsesStopwatch,
				ShouldAutoCancel = settings.ShouldAutoCancel,
				Number = settings.Number,
				SortKey = settings.SortKey,
				Color = settings.Color,
				Style = (NotificationStyle)(int)settings.Style,
				SmallIcon = settings.SmallIcon,
				LargeIcon = settings.LargeIcon,
			};

			var trigger = settings.Trigger;

			switch (trigger.Type)
			{
				case NotificationTrigger.TriggerType.TimeInterval:
				case NotificationTrigger.TriggerType.ConcreteDayTime:
				{
					if (trigger.Repeat)
						androidNotification.RepeatInterval = trigger.Interval;
					else
						androidNotification.RepeatInterval = null;
				}
				break;

				default:
				{
					androidNotification.RepeatInterval = null;
				}
				break;
			}

			androidNotification.FireTime = trigger.TriggerTime;

			return androidNotification;
		}

		public static ILocalNotification ToLocal(AndroidNotificationIntentData androidNotification)
		{
			var settings = GetSettings(androidNotification.Notification);
			settings.ChannelId = androidNotification.Channel;
			settings.Id = androidNotification.Id;

			var title = androidNotification.Notification.Title;
			var body = androidNotification.Notification.Text;
			var data = NotificationDataHelper.FromString<Dictionary<string, string>>(androidNotification.Notification.IntentData);

			return new LocalNotification(title, body, settings, data);
		}

		private static AndroidNotificationSettingsWrapper GetSettings(AndroidNotification notification)
		{
			AndroidNotificationSettingsWrapper settings = new AndroidNotificationSettingsWrapper();

			settings.GroupAlertBehaviour = (AndroidGroupAlertBehaviours)(int)notification.GroupAlertBehaviour;
			settings.GroupSummary = notification.GroupSummary;
			settings.Group = notification.Group;

			settings.UsesStopwatch = notification.UsesStopwatch;
			settings.ShouldAutoCancel = notification.ShouldAutoCancel;
			settings.Number = notification.Number;
			settings.SortKey = notification.SortKey;

			settings.Color = notification.Color;
			settings.Style = (AndroidNotificationStyle)(int)notification.Style;
			settings.SmallIcon = notification.SmallIcon;
			settings.LargeIcon = notification.LargeIcon;

			return settings;
		}
	}
}
#endif