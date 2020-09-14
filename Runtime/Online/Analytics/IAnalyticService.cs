namespace REF.Runtime.Online.Analytics
{
	public interface IAnalyticService : IOnlineService
	{
		bool IsValidEvent(string eventName);
		bool IsValidParameter(Parameter parameter);

		void SetUserId(string id);
		void SetScreenName(string name);
		void SendEvent(string name, Parameter[] parameters);
	}
}
