#if REF_ONLINE_PUSH_NOTIFICATION && REF_FIREBASE_PUSH_NOTIFICATION && REF_USE_FIREBASE

using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using REF.Runtime.Core;
using REF.Runtime.Diagnostic;
using REF.Runtime.Notifications;
using REF.Runtime.Online.Service;

using Firebase.Messaging;

namespace REF.Runtime.Online.Notifications
{
	public class FirebasePushNotificationService : FirebaseService, IPushNotificationService
	{
		public event System.Action<string> OnTokenReceived;
		public event System.Action<INotification> OnNotificationReceived;

		private string token = string.Empty;
		private List<string> subscriptions = new List<string>();

		public override void Configure(IConfiguration config)
		{
			base.Configure(config);

			var configuration = config as IPushConfiguration;

			if (configuration == null)
			{
				RefDebug.Error(nameof(FirebasePushNotificationService), $"Config must be of type {nameof(IPushConfiguration)}!");
				return;
			}

			var topicConfig = configuration.GetSubscriptionTopics();
			subscriptions = topicConfig.ToList();
		}

		public string GetToken()
		{
			return token;
		}

		public void Subscribe(string topic)
		{
			Task subscriptionTask = FirebaseMessaging.SubscribeAsync(topic);
			subscriptionTask.ContinueWith(TaskComplete);
		}

		public void Unsubscribe(string topic)
		{
			Task unsubscriptionTask = FirebaseMessaging.UnsubscribeAsync(topic);
			unsubscriptionTask.ContinueWith(TaskComplete);
		}

		public override void Release(System.Action callback)
		{
			if (IsInitialized())
			{
				FirebaseMessaging.TokenReceived -= OnTokenReceivedHandler;
				FirebaseMessaging.MessageReceived -= OnMessageReceivedHandler;
			}

			base.Release(callback);
		}

		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			if (successful)
			{
				Task permissionRequestTask = FirebaseMessaging.RequestPermissionAsync();
				permissionRequestTask.ContinueWith(TaskComplete);

				if (subscriptions != null)
				{
					for (int idx = 0; idx < subscriptions.Count; ++idx)
					{
						var topic = subscriptions[idx];
						Subscribe(topic);
					}
				}

				FirebaseMessaging.TokenReceived += OnTokenReceivedHandler;
				FirebaseMessaging.MessageReceived += OnMessageReceivedHandler;
			}

			callback?.Invoke();
		}

		private void OnMessageReceivedHandler(object sender, MessageReceivedEventArgs e)
		{
			if (e.Message != null)
			{
				var firebaseNotification = e.Message.Notification; // if it's null then we received a data message

				string title = firebaseNotification != null ? firebaseNotification.Title : string.Empty;
				string body = firebaseNotification != null ? firebaseNotification.Body : string.Empty;

				Notification notification = new Notification(title, body, e.Message.Data);
				OnNotificationReceived?.Invoke(notification);
			}
		}

		private void OnTokenReceivedHandler(object sender, TokenReceivedEventArgs e)
		{
			token = e.Token;
			OnTokenReceived?.Invoke(e.Token);
		}

		private void TaskComplete(Task task)
		{
			
		}
	}
}

#endif