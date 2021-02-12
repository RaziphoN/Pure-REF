#if REF_ONLINE_DYNAMIC_LINK

using REF.Runtime.Core;

namespace REF.Runtime.Online.DynamicLinks
{
	public interface IDynamicLinkService : IOnlineService<IConfiguration>
	{
		event System.Action<System.Uri> OnLinkReceived;

		void RequestShortLink(DynamicLink longLink, System.Action<System.Uri> onLinkCreated, System.Action onRequestFailed = null);
	}
}

#endif
