#if REF_ONLINE_PUSH_NOTIFICATION

using REF.Runtime.Core;

namespace REF.Runtime.Online.Notifications
{
	public interface IPushConfiguration : IConfiguration
	{
		string[] GetSubscriptionTopics();
	}
}

#endif
