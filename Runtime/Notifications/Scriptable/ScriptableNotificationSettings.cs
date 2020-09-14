using UnityEngine;

using REF.Runtime.Notifications.iOS;
using REF.Runtime.Notifications.Android;
using REF.Runtime.Utilities.Serializable.Nullable;

namespace REF.Runtime.Notifications
{
	[CreateAssetMenu(fileName = "Notification Settings", menuName = "REF/Notifications/NotificationSettings")]
	public class ScriptableNotificationSettings : ScriptableObject, IAndroidNotificationSettings, IIosNotificationSettings
	{
		[Header("Android ------------------------------------------------------------------------------------------------------------------------")]
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

		[Header("IOS ------------------------------------------------------------------------------------------------------------------------")]

		[SerializeField] private string identifier = string.Empty;
		[SerializeField] private string subtitle = string.Empty;

		[Space]

		// Visualization
		[SerializeField] private int badge = -1;
		[SerializeField] private bool showInForeground = false;
		[SerializeField] private iOSPresentationOption presentationOption = iOSPresentationOption.None;

		[Header("----------------------------------------------------------------------------------------------------------------------------")]

		[SerializeField] private NotificationTrigger trigger = new NotificationTrigger();

		private INotificationSettings cached = null;
		private bool isSet = false;

		// IOS
		public string Identifier { get { return identifier; } set { identifier = value; } }
		public string Subtitle { get { return subtitle; } set { subtitle = value; } }
		public bool ShowInForeground { get { return showInForeground; } set { showInForeground = value; } }
		public iOSPresentationOption ForegroundPresentationOption { get { return presentationOption; } set { presentationOption = value; } }
		public int Badge { get { return badge; } set { badge = value; } }

		// Android
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

		public bool IsRemote { get; } = false;
		public NotificationTrigger Trigger { get { return trigger; } }

		NotificationId INotificationSettings.NotificationId
		{
			get
			{
#if UNITY_ANDROID
				return new NotificationId(Id);
#elif UNITY_IOS
				return new NotificationId(Identifier);
#endif
				return new NotificationId();
			}
		}

		public INotificationSettings Settings
		{
			get
			{
				if (isSet)
					return cached;

				return this;
			}

			set
			{
				cached = value;
				isSet = true;
			}
		}
	}
}
