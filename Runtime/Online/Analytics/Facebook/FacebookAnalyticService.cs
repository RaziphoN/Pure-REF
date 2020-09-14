using UnityEngine;

using REF.Runtime.Online.Service.Facebook;

using System.Collections.Generic;

using Facebook.Unity;

namespace REF.Runtime.Online.Analytics.Facebook
{
	public class FacebookAnalyticService : FacebookService, IAnalyticService
	{
		[SerializeField] private bool autoLogAppEvents = true;
		[SerializeField] private bool advertiserIdCollection = true;

		public bool IsValidEvent(string eventName)
		{
			return true;
		}

		public bool IsValidParameter(Parameter parameter)
		{
			return true;
		}

		public void SendEvent(string name, Parameter[] parameters)
		{
			Dictionary<string, object> converted = ConvertParams(parameters);
			FB.LogAppEvent(name, null, converted);
		}

		public void SetScreenName(string name)
		{
			var parameters = new Dictionary<string, object>();
			parameters.Add(AppEventParameterName.ContentID, name);

			FB.LogAppEvent(AppEventName.ViewedContent, null, parameters);
		}

		public void SetUserId(string id)
		{
			// It's not implemented
		}

		public override void OnApplicationPause(bool pause)
		{
			base.OnApplicationPause(pause);

			if (!pause && IsInitialized())
			{
				FB.ActivateApp();
			}
		}

		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			if (successful)
			{
				FB.Mobile.SetAutoLogAppEventsEnabled(autoLogAppEvents);
				FB.Mobile.SetAdvertiserIDCollectionEnabled(advertiserIdCollection);
				FB.ActivateApp();
			}

			callback?.Invoke();
		}

		private Dictionary<string, object> ConvertParams(Parameter[] parameters)
		{
			Dictionary<string, object> result = new Dictionary<string, object>();

			for (int idx = 0; idx < parameters.Length; ++idx)
			{
				var parameter = parameters[idx];
				var value = parameter.Value;

				switch (value.GetValueType())
				{
					case Type.Bool:
					{
						result.Add(parameter.Name, value.GetBool() ? 1 : 0);
					}
					break;

					case Type.Long:
					{
						result.Add(parameter.Name, value.GetLong());
					}
					break;

					case Type.Double:
					{
						result.Add(parameter.Name, (float)value.GetDouble());
					}
					break;

					case Type.String:
					{
						result.Add(parameter.Name, value.GetString());
					}
					break;

					default:
					{

					}
					break;
				}
			}

			return result;
		}
	}
}
