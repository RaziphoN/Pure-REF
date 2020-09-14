using UnityEngine;

namespace REF.Runtime.Notifications.Android
{
	public enum AndroidGroupAlertBehaviours
	{
		GroupAlertAll = 0,
		GroupAlertSummary = 1,
		GroupAlertChildren = 2,
	}

	public enum AndroidNotificationStyle
	{
		None = 0,
		BigTextStyle = 2
	}

	public interface IAndroidNotificationSettings : INotificationSettings
	{
		int Id { get; set; }
		string ChannelId { get; set; }

		AndroidGroupAlertBehaviours GroupAlertBehaviour { get; set; }
		bool GroupSummary { get; set; }
		string Group { get; set; }

		bool UsesStopwatch { get; set; }
		bool ShouldAutoCancel { get; set; }
		int Number { get; set; }
		string SortKey { get; set; }

		Color? Color { get; set; }
		AndroidNotificationStyle Style { get; set; }

		string SmallIcon { get; set; }
		string LargeIcon { get; set; }
	}
}
