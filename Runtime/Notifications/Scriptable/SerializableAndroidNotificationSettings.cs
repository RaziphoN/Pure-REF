using UnityEngine;

using REF.Runtime.Utilities.Serializable.Nullable;
using REF.Runtime.Notifications.Android;

namespace REF.Runtime.Notifications
{
	[System.Serializable]
	public class SerializableAndroidNotificationSettings : IAndroidNotificationSettings
	{
		// This is a notification id. If it -1 then notification manager will create notification id for you automatically. Otherwise it will register notification under your id
		[Min(-1)]
		[SerializeField] private int id = -1;

		// This is an android notification channel id you want to schedule this notification in. Our manager will register channel if it wasn't previously registered
		[SerializeField] private string channeId = "DefaultGameChannel";

		[Space]

		[SerializeField] private AndroidGroupAlertBehaviours groupAlertBehaviour = AndroidGroupAlertBehaviours.GroupAlertAll;
		[SerializeField] private bool groupSummary = false;
		[SerializeField] private string group = string.Empty;
		[SerializeField] private string sortKey = string.Empty;

		[Space]

		// Visualization
		[SerializeField] private AndroidNotificationStyle style = AndroidNotificationStyle.None;
		[SerializeField] private string smallIcon = string.Empty;
		[SerializeField] private string largeIcon = string.Empty;
		[SerializeField] private NullableColor color = new NullableColor();

		[Space]

		//Flags
		[SerializeField] private int number = 0;
		[SerializeField] private bool usesStopwatch = false;
		[SerializeField] private bool shouldAutoCancel = false;

		[Space]

		// Notification Time
		[SerializeField] private NotificationTrigger trigger = new NotificationTrigger();

		public int Id
		{
			get
			{
				return id;
			}

			set
			{
				if (value < 0)
					value = -1;
				id = value;
			}
		}

		public NotificationId NotificationId { get { return new NotificationId(Id); } }
		public bool IsRemote { get; } = false;
		public string ChannelId { get { return channeId; } set { channeId = value; } }
		public AndroidGroupAlertBehaviours GroupAlertBehaviour { get { return groupAlertBehaviour; } set { groupAlertBehaviour = value; } }
		public bool GroupSummary { get { return groupSummary; } set { groupSummary = value; } }
		public string Group { get { return group; } set { group = value; } }
		public bool UsesStopwatch { get { return usesStopwatch; } set { usesStopwatch = value; } }
		public bool ShouldAutoCancel { get { return shouldAutoCancel; } set { shouldAutoCancel = value; } }
		public int Number { get { return number; } set { number = value; } }
		public string SortKey { get { return sortKey; } set { sortKey = value; } }
		public AndroidNotificationStyle Style { get { return style; } set { style = value; } }
		public string SmallIcon { get { return smallIcon; } set { smallIcon = value; } }
		public string LargeIcon { get { return largeIcon; } set { largeIcon = value; } }

		public Color? Color
		{
			get
			{
				if (color.HasValue)
					return color.Value.Value;

				return null;
			}

			set
			{
				if (value == null)
					color.Value = null;
				else
					color.Value = value;
			}
		}

		public NotificationTrigger Trigger { get { return trigger; } }
	}
}
