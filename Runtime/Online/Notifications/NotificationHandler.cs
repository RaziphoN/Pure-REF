#if REF_ONLINE_PUSH_NOTIFICATION

using System.Linq;
using System.Collections.Generic;

using REF.Runtime.Notifications;

namespace REF.Runtime.Online.Notifications
{
	public class NotificationHandler : INotificationProcessor
	{
		private List<INotificationProcessor> processors = new List<INotificationProcessor>();
		private bool allowMultipleProcessorPerLink = true;

		public int Priority { get { return int.MaxValue; } }

		public void SetMultiProcessingEnabled(bool enabled)
		{
			allowMultipleProcessorPerLink = enabled;
		}

		public void Register(INotificationProcessor processor)
		{
			if (!processors.Contains(processor))
			{
				processors.Add(processor);
				ReOrderProcessors();
			}
		}

		public void Unregister(INotificationProcessor processor)
		{
			if (processors.Contains(processor))
				processors.Remove(processor);
		}

		public void Handle(INotification notification)
		{
			if (notification == null)
				return;

			foreach (INotificationProcessor processor in processors)
			{
				if (processor.IsApplicable(notification))
				{
					processor.Handle(notification);

					if (!allowMultipleProcessorPerLink)
						break;
				}
			}
		}

		private void ReOrderProcessors()
		{
			processors = processors.OrderByDescending(processor => processor.Priority).ToList();
		}

		public bool IsApplicable(INotification notification)
		{
			return true;
		}
	}
}

#endif