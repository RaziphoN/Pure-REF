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
		int Id { get; }
		string ChannelId { get; }

		AndroidGroupAlertBehaviours GroupAlertBehaviour { get; }
		bool GroupSummary { get; }
		string Group { get; }

		bool UsesStopwatch { get; }
		bool ShouldAutoCancel { get; }
		int Number { get; }
		string SortKey { get; }

		Color? Color { get; }
		AndroidNotificationStyle Style { get; }

		string SmallIcon { get; }
		string LargeIcon { get; }
	}
}
