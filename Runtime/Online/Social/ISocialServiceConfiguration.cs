#if REF_ONLINE_SOCIAL

using REF.Runtime.Core;

namespace REF.Runtime.Online.Social
{
	public interface ISocialServiceConfiguration : IConfiguration
	{
		string[] GetPermissions();

		int GetAvatarWidth();
		int GetAvatarHeight();
	}
}

#endif
