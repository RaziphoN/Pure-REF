#if REF_ONLINE_IN_APP_MESSAGING && REF_ONLINE_IN_APP_MESSAGING && REF_USE_FIREBASE

using REF.Runtime.Online.Service;

namespace REF.Runtime.Online.InAppMessaging
{
	public class FirebaseInAppMessaging : FirebaseService
	{
		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			callback?.Invoke();
		}
	}
}

#endif