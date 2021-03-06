﻿#if REF_ONLINE_ANALYTICS

namespace REF.Runtime.Online.Analytics
{
	public interface IAnalyticsService : IOnlineService
	{
		bool IsValidEvent(string eventName);
		bool IsValidParameter(Parameter parameter);

		void SetUserId(string id);
		void SetScreenName(string name);
		void SendEvent(string name, Parameter[] parameters);
	}
}

#endif
