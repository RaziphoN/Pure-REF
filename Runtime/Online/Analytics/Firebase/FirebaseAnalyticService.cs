#if REF_ONLINE_ANALYTICS && REF_FIREBASE_ANALYTICS && REF_USE_FIREBASE
using System.Linq;

using REF.Runtime.Core;
using REF.Runtime.Diagnostic;
using REF.Runtime.Online.Service;

using Firebase.Analytics;

namespace REF.Runtime.Online.Analytics
{
	public class FirebaseAnalyticService : FirebaseService, IAnalyticsService
	{
		private bool isAutoLogEnabled = true;

		private string setId;
		private string setScreenName;

		public override void Configure(IConfiguration config)
		{
			base.Configure(config);

			var configuration = config as IAnalyticsConfiguration;

			if (configuration == null)
			{
				RefDebug.Error(nameof(FirebaseAnalyticService), $"Config must be of type {nameof(IAnalyticsConfiguration)}!");
				return;
			}

			isAutoLogEnabled = configuration.IsAutoLogEnabled();
		}

		public void SetUserId(string id)
		{
			if (FirebaseInitializer.AllowApiCalls)
			{
				FirebaseAnalytics.SetUserId(id);
			}
		}

		public void SetScreenName(string name)
		{
			if (FirebaseInitializer.AllowApiCalls)
			{
				FirebaseAnalytics.SetCurrentScreen(name, "N/A");
			}
		}

		public bool IsValidEvent(string eventName)
		{
			return eventName.Length <= 40;
		}

		public bool IsValidParameter(Parameter parameter)
		{
			if (parameter.Value.GetValueType() == Type.String)
			{
				if (!(parameter.Value.GetString().Length < 100))
					return false;
			}

			return parameter.Name.Length <= 40;
		}

		public void SendEvent(string name, Parameter[] parameters)
		{
			if (!FirebaseInitializer.AllowApiCalls)
			{
				return;
			}

			if (IsValidEvent(name) && parameters.All((parameter) => { return IsValidParameter(parameter); }))
			{
				var firebaseParameters = ConvertParams(parameters);
				FirebaseAnalytics.LogEvent(name, firebaseParameters);
			}
		}

		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			FirebaseAnalytics.SetAnalyticsCollectionEnabled(isAutoLogEnabled);
			SetInitialized(true);
			callback?.Invoke();
		}

		private Firebase.Analytics.Parameter[] ConvertParams(Parameter[] parameters)
		{
			Firebase.Analytics.Parameter[] result = new Firebase.Analytics.Parameter[parameters.Length];

			for (int i = 0; i < parameters.Length; ++i)
			{
				var param = parameters[i];

				switch (param.Value.GetValueType())
				{
					case Type.Bool:
					result[i] = new Firebase.Analytics.Parameter(param.Name, param.Value.GetBool().ToString());
					break;

					case Type.Long:
					result[i] = new Firebase.Analytics.Parameter(param.Name, param.Value.GetLong());
					break;

					case Type.Double:
					result[i] = new Firebase.Analytics.Parameter(param.Name, param.Value.GetDouble());
					break;

					case Type.String:
					result[i] = new Firebase.Analytics.Parameter(param.Name, param.Value.GetString());
					break;
				}
			}

			return result;
		}
	}
}

#endif