using System.Threading.Tasks;

using Firebase;

namespace REF.Runtime.Online
{
	public static class FirebaseInitializer
	{
		public static event System.Action<bool> OnInitialized;

		public static bool IsInitializationPerformed { get { return isInitializationPerformed; } }
		public static bool AllowApiCalls { get { return allowApiCalls && isInitializationPerformed; } }
		public static FirebaseApp App { get; private set; }

		private static bool allowApiCalls = false;
		private static bool isInitializationPerformed = false;
		private static bool shouldInitialize = true;

		public static void Initialize()
		{
			if (shouldInitialize)
			{
				shouldInitialize = false;
				Task<DependencyStatus> asyncTask = FirebaseApp.CheckAndFixDependenciesAsync();
				asyncTask.ContinueWith(OnDependenciesResolvedHandler);
			}
		}

		public static void Release()
		{
			if (IsInitializationPerformed)
			{
				isInitializationPerformed = false;
				shouldInitialize = true;
				App?.Dispose();
			}
		}

		private static void OnDependenciesResolvedHandler(Task<DependencyStatus> task)
		{
			FinalizeInit(task.Result == DependencyStatus.Available);
		}

		private static void FinalizeInit(bool successful)
		{
			if (successful)
				OnSuccess();
			else
				OnFail();

			isInitializationPerformed = true;
			OnInitialized?.Invoke(successful);
		}

		private static void OnSuccess()
		{
			App = FirebaseApp.DefaultInstance;
			allowApiCalls = true;
		}

		private static void OnFail()
		{
			shouldInitialize = true;
			allowApiCalls = false;
		}
	}
}