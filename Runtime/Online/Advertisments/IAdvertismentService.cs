#if REF_ONLINE_ADVERTISMENT

using REF.Runtime.Core;

namespace REF.Runtime.Online.Advertisments
{
	public interface IAdvertismentService : IOnlineService<IConfiguration>
	{
		void SetTestAds(bool value);

		IBannerAd CreateBanner(string placement, BannerSettings settings);
		IInterstitialAd CreateInterstitial(string placement);
		IRewardedAd CreateRewarded(string placement);
	}
}

#endif
