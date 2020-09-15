#if REF_ONLINE_SOCIAL

using UnityEngine;

namespace REF.Runtime.Online.Social
{
	public interface ISocialUserProfile
	{
		string GetUserId();
		string GetDisplayName();
		string GetAvatarUrl();
	}
}

#endif
