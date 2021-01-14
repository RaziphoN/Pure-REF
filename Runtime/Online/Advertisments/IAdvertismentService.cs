#if REF_ONLINE_ADVERTISMENT

namespace REF.Runtime.Online.Advertisments
{
	public interface IAdvertismentService : IOnlineService
	{
		void SetTestAds(bool value);

		IBannerAd CreateBanner(string placement, BannerSettings settings);
		IInterstitialAd CreateInterstitial(string placement);
		IRewardedAd CreateRewarded(string placement);
	}
}

#endif
