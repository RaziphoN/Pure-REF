#if REF_USE_FACEBOOK

using Facebook.Unity;

namespace REF.Runtime.Online.Service.Facebook
{
	public static class FacebookInitializer
	{
		public static event System.Action<bool> OnInitialized;

		private static bool shouldInitialize = false;
		private static bool initialized = false;

		public static bool IsInitialized()
		{
			return initialized;
		}

		public static bool AllowApiCalls()
		{
			return FB.IsInitialized;
		}

		public static void Initialize()
		{
			if (shouldInitialize)
			{
				shouldInitialize = false;
				FB.Init(OnInitializedHandler);
			}
		}

		public static void Release()
		{
			if (shouldInitialize)
			{
				shouldInitialize = true;
				initialized = false;
			}
		}

		private static void OnInitializedHandler()
		{
			initialized = true;
			OnInitialized?.Invoke(FB.IsInitialized);
		}
	}
}

#endif