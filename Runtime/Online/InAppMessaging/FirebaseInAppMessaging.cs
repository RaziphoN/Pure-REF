#if REF_ONLINE_IN_APP_MESSAGING && REF_ONLINE_IN_APP_MESSAGING && REF_USE_FIREBASE

using UnityEngine;

using REF.Runtime.Online.Service;

namespace REF.Runtime.Online.InAppMessaging
{
	[CreateAssetMenu(fileName = "FirebaseInAppMessagingService", menuName = "REF/Online/In App Messaging/Firebase InAppMessaging")]
	public class FirebaseInAppMessaging : FirebaseService
	{
		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			callback?.Invoke();
		}
	}
}

#endif