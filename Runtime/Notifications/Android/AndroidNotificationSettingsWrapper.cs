using UnityEngine;

using REF.Runtime.Utilities.Serializable.Nullable;

namespace REF.Runtime.Notifications.Android
{
	[System.Serializable]
	public class AndroidNotificationSettingsWrapper : IAndroidNotificationSettings
	{
		[SerializeField, Min(-1)] private int id = -1;
		[SerializeField] private int number;
		[SerializeField] private string channelId = "Default";
		[SerializeField] private string sortKey;

		[SerializeField] private bool isRemote;

		[SerializeField] private string group;
		[SerializeField] private AndroidGroupAlertBehaviours groupAlertBehaviour = AndroidGroupAlertBehaviours.GroupAlertAll;
		[SerializeField] private bool groupSummary;

		[SerializeField] private AndroidNotificationStyle style = AndroidNotificationStyle.None;
		[SerializeField] private NullableColor color = new NullableColor();
		[SerializeField] private string smallIcon;
		[SerializeField] private string largeIcon;

		[SerializeField] private bool useStopwatch;
		[SerializeField] private bool autoCancel;

		[SerializeField] private NotificationTrigger trigger = new NotificationTrigger();

		public bool IsRemote { get { return isRemote; } internal set { isRemote = value; } }

		public NotificationId NotificationId { get { return new NotificationId(Id); } }
		public int Id { get { return id; } set { id = value; } }
		public int Number { get { return number; } set { number = value; } }
		public string ChannelId { get { return channelId; } set { channelId = value; } }
		public string SortKey { get { return sortKey; } set { sortKey = value; } }

		public string Group { get { return group; } set { group = value; } }
		public AndroidGroupAlertBehaviours GroupAlertBehaviour { get { return groupAlertBehaviour; } set { groupAlertBehaviour = value; } }
		public bool GroupSummary { get { return groupSummary; } set { groupSummary = value; } }

		public AndroidNotificationStyle Style { get { return style; } set { style = value; } }
		public Color? Color { get { return color.Value;  } set { color.Value = value; } }
		public string SmallIcon { get { return smallIcon; } set { smallIcon = value; } }
		public string LargeIcon { get { return largeIcon; } set { largeIcon = value; } }

		public bool UsesStopwatch { get { return useStopwatch; } set { useStopwatch = value; } }
		public bool ShouldAutoCancel { get { return autoCancel; } set { autoCancel = value; } }

		public NotificationTrigger Trigger { get { return trigger; } set { trigger = value; } }
	}
}
