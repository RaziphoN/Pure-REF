#if REF_USE_FACEBOOK

using REF.Runtime.Core;

namespace REF.Runtime.Online.Service.Facebook
{
	public class FacebookService : OnlineService<IConfiguration>
	{
		private System.Action postponedCallback = null;

		public override void PreInitialize(System.Action callback)
		{
			if (!FacebookInitializer.IsInitialized())
			{
				FacebookInitializer.OnInitialized += OnFacebookInitializedHandler;
				FacebookInitializer.Initialize();
			}
			else
			{
				SetInitialized(FacebookInitializer.AllowApiCalls());
			}

			callback?.Invoke();
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