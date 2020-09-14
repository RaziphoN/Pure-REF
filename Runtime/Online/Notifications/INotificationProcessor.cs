
using REF.Runtime.Notifications;

namespace REF.Runtime.Online.Notifications
{
	public interface INotificationProcessor
	{
		int Priority { get; }

		bool IsApplicable(INotification notification);
		void Handle(INotification notification);
	}
}
