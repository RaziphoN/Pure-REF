using UnityEngine;

namespace REF.Runtime.Notifications
{
	[CreateAssetMenu(fileName = "Notification Settings", menuName = "REF/Notifications/NotificationSettings")]
	public class ScriptableNotificationSettings : ScriptableObject
	{
		[SerializeField] private NotificationSettings settings = new NotificationSettings();

		public INotificationSettings ToSettings()
		{
			return settings.ToSettings();
		}
	}
}
