#if REF_ONLINE_DYNAMIC_LINK

namespace REF.Runtime.Online.DynamicLinks
{
	public class AndroidLinkParams
	{
		public AndroidLinkParams(string package)
		{
			PackageName = package;
		}

		public string PackageName { get; set; }
		public string FallbackUri { get; set; }
	}
}

#endif