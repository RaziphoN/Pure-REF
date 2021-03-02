using UnityEngine;

using REF.Runtime.Notifications.Android;
using REF.Runtime.Notifications.iOS;

namespace REF.Runtime.Notifications
{
	[System.Serializable]
	public class NotificationSettings
	{
		[SerializeField] private AndroidNotificationSettingsWrapper androidSettings = new AndroidNotificationSettingsWrapper();
		[SerializeField] private iOSNotificationSettingsWrapper iOSSettings = new iOSNotificationSettingsWrapper();
		[SerializeField] private NotificationTrigger trigger;

		public INotificationSettings ToSettings()
		{
			androidSettings.Trigger = trigger;
			iOSSettings.Trigger = trigger;

#if UNITY_ANDROID
			return androidSettings;
#elif UNITY_IOS
			return iOSSettings;
#else
			return null;
#endif
		}
	}
}
