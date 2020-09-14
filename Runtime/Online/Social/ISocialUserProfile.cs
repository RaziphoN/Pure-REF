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
