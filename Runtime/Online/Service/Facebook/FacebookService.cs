#if REF_USE_FACEBOOK

namespace REF.Runtime.Online.Service.Facebook
{
	public class FacebookService : OnlineService
	{
		private System.Action postponedCallback = null;

		public override void Initialize(System.Action callback)
		{
			if (!FacebookInitializer.IsInitialized())
			{
				FacebookInitializer.OnInitialized += OnFacebookInitializedHandler;
				FacebookInitializer.Initialize();
			}
			else
			{
				OnFacebookInitializedHandler(FacebookInitializer.AllowApiCalls());
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
				postponedCallback = callback;
			}
		}

		private void OnFacebookInitializedHandler(bool successful)
		{
			FinalizeInit(successful, () =>
			{
				SetInitialized(successful);

				postponedCallback?.Invoke();
				postponedCallback = null;
			});
		}

		protected virtual void FinalizeInit(bool successful, System.Action callback)
		{
			callback?.Invoke();
		}
	}
}

#endif