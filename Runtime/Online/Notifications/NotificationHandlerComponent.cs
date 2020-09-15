#if REF_ONLINE_PUSH_NOTIFICATION

using UnityEngine;

using System.Collections.Generic;

using REF.Runtime.Notifications;

namespace REF.Runtime.Online.Notifications
{
	public abstract class NotificationHandlerComponent<T> : MonoBehaviour, INotificationProcessor where T : Object, INotificationProcessor
	{
		[SerializeField] private List<T> handlers = new List<T>();
		private NotificationHandler internalHandler = new NotificationHandler();

		public int Priority { get { return internalHandler.Priority; } }

		public void Handle(INotification notification)
		{
			internalHandler.Handle(notification);
		}

		public bool IsApplicable(INotification notification)
		{
			return internalHandler.IsApplicable(notification);
		}

		private void Awake()
		{
			foreach (var handler in handlers)
				internalHandler.Register(handler);
		}

		private void OnDisable()
		{
			foreach (var handler in handlers)
				internalHandler.Unregister(handler);
		}
	}
}

#endif