#if REF_ONLINE_PUSH_NOTIFICATION

using System.Collections.Generic;

using REF.Runtime.Core;

namespace REF.Runtime.Online.Notifications
{
	public interface IPushConfiguration : IConfiguration
	{
		IEnumerable<string> GetSubscriptionTopics();
	}
}

#endif
