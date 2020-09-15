#if REF_ONLINE_DYNAMIC_LINK

namespace REF.Runtime.Online.DynamicLinks
{
	public class IosLinkParams
	{
		public IosLinkParams(string bundleId)
		{
			BundleId = bundleId;
			IPadBundleId = bundleId;
		}

		public string AppStoreId { get; set; }
		public string CustomScheme { get; set; }

		public string BundleId { get; set; }
		public string FallbackUri { get; set; }

		public string IPadFallbackUri { get; set; }
		public string IPadBundleId { get; set; }

	}
}

#endif
