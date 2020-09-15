#if REF_USE_FIREBASE

using UnityEngine;

namespace REF.Runtime.Online.Service.Firebase
{
	public class FirebaseInitializerComponent : MonoBehaviour
	{
		private void Awake()
		{
			FirebaseInitializer.Initialize();
		}

		private void OnApplicationQuit()
		{
			FirebaseInitializer.Release();
		}
	}
}

#endif
