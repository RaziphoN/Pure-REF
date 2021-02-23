#if REF_ONLINE_DYNAMIC_LINK

using REF.Runtime.Core;

namespace REF.Runtime.Online.DynamicLinks
{
	public interface IDynamicLinkConfiguration : IConfiguration
	{
		string GetLinkPrefix();
	}
}

#endif
