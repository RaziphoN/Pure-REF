using UnityEngine;

using System.Threading.Tasks;

using REF.Runtime.Notifications;
using REF.Runtime.Online.Service;

using Firebase.Messaging;

namespace REF.Runtime.Online.Notifications
{
	[CreateAssetMenu(fileName = "FirebasePushNotificationService", menuName = "REF/Online/Push Notifications/Firebase Push Notifications")]
	public class FirebasePushNotificationService : FirebaseService, IPushNotificationService
	{
		public event System.Action<string> OnTokenReceived;
		public event System.Action<INotification> OnNotificationReceived;

		[SerializeField] private string allTopic = "all";
		private string token = "";

		public string GetToken()
		{
			return token;
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
				Task subscriptionTask = FirebaseMessaging.SubscribeAsync(allTopic);
				Task permissionRequestTask = FirebaseMessaging.RequestPermissionAsync();

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
	}
}
