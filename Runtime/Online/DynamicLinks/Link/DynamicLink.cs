#if REF_ONLINE_DYNAMIC_LINK

using System.Collections.Generic;

namespace REF.Runtime.Online.DynamicLinks
{
	public class DynamicLink
	{
		public DynamicLink(string baseUri)
		{
			BaseUri = baseUri;
		}

		public string BaseUri { get; set; }
		public AndroidLinkParams AndroidParams { get; set; }
		public IosLinkParams IosParams { get; set; }
		public SocialLinkParams Social { get; set; }
		public IDictionary<string, string> Data { get; set; }
	}
}

#endif
