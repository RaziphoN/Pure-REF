using REF.Runtime.Core;

namespace REF.Runtime.Notifications
{
	public class LocalNotificationService : ServiceBase, ILocalNotificationService
	{
		public event System.Action<ILocalNotification> OnLocalNotificationReceived;

		public bool IsScheduled(NotificationId id)
		{
			return LocalNotificationWrapper.IsLocal(id);
		}

		public ILocalNotification GetLastNotification()
		{
			return LocalNotificationWrapper.GetLastNotification();
		}

		public NotificationId Schedule(ILocalNotification notification)
		{
			return LocalNotificationWrapper.Schedule(notification);
		}

		public void CancelScheduled(NotificationId id)
		{
			LocalNotificationWrapper.CancelScheduled(id);
		}

		public void CancelDisplayed(NotificationId id)
		{
			LocalNotificationWrapper.CancelDisplayed(id);
		}

		public void CancelAllScheduled()
		{
			LocalNotificationWrapper.CancelAllScheduled();
		}

		public void CancelAllDisplayed()
		{
			LocalNotificationWrapper.CancelAllDisplayed();
		}

		public void CancelAll()
		{
			CancelAllScheduled();
			CancelAllDisplayed();
		}

		public override void Construct(IApp app)
		{
			base.Construct(app);
			LocalNotificationWrapper.Subscribe(OnNotificationReceivedHandler);
		}

		public override void Initialize(System.Action callback)
		{
			base.Initialize(() =>
			{
				LocalNotificationWrapper.Initialize();
				callback?.Invoke();
			});
		}

		public override void Release(System.Action callback)
		{
			LocalNotificationWrapper.Unsubscribe(OnNotificationReceivedHandler);
			LocalNotificationWrapper.Release();
			base.Release(callback);
		}

		private void OnNotificationReceivedHandler(ILocalNotification notification)
		{
			OnLocalNotificationReceived?.Invoke(notification);
		}
	}
}
