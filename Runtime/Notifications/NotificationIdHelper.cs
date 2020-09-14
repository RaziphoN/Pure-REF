using UnityEngine;

namespace REF.Runtime.Notifications
{
	public static class NotificationIdHelper
	{
		public static void SavePref(string key, NotificationId id)
		{
#if UNITY_ANDROID
			PlayerPrefs.SetInt(key, id.AndroidId);
#elif UNITY_IOS
			PlayerPrefs.SetString(key, id.iOSId);
#endif
		}

		public static NotificationId LoadPref(string key)
		{
			var notificationId = new NotificationId();

#if UNITY_ANDROID
			int androidId = PlayerPrefs.GetInt(key, notificationId.AndroidId);
			notificationId = new NotificationId(androidId);
#elif UNITY_IOS
			string iOSId = PlayerPrefs.GetString(key, notificationId.iOSId);
			notificationId = new NotificationId(iOSId);
#endif

			return notificationId;
		}
	}
}
