
namespace REF.Runtime.Online.DynamicLinks
{
	public interface IDynamicLinkService : IOnlineService
	{
		event System.Action<System.Uri> OnLinkReceived;

		void RequestShortLink(DynamicLink longLink, System.Action<System.Uri> onLinkCreated, System.Action onRequestFailed = null);
	}
}
