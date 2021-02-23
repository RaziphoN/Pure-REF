#if REF_USE_FIREBASE

namespace REF.Runtime.Online.Service
{
	public abstract class FirebaseService : OnlineService
	{
		private System.Action posponedCallback = null;

		public override void Initialize(System.Action callback)
		{
			FirebaseInitializer.OnInitialized += OnFirebaseInitializedHandler;

			if (FirebaseInitializer.IsInitializationPerformed)
			{
				OnFirebaseInitializedHandler(FirebaseInitializer.AllowApiCalls);
			}
			else
			{
				FirebaseInitializer.Initialize();
			}

			base.Initialize(callback);
		}

		public override void PostInitialize(System.Action callback)
		{
			if (IsInitialized())
			{
				callback?.Invoke();
			}
			else
			{
				posponedCallback = callback;
			}
		}

		public override void Release(System.Action callback)
		{
			if (IsInitialized())
			{
				FirebaseInitializer.OnInitialized -= OnFirebaseInitializedHandler;
				FirebaseInitializer.Release();
			}

			base.Release(callback);
		}

		// NOTE: init concrete firebase service here
		protected abstract void FinalizeInit(bool successful, System.Action callback);

		private void OnFirebaseInitializedHandler(bool successful)
		{
			FirebaseInitializer.OnInitialized -= OnFirebaseInitializedHandler;
			FinalizeInit(successful, () =>
			{
				SetInitialized(successful);

				posponedCallback?.Invoke();
				posponedCallback = null;
			});
		}
	}
}

#endif