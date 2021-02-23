#if REF_ONLINE_ADVERTISMENT && REF_ADMOB_ADVERTISMENT && REF_USE_ADMOB

using UnityEngine;

using REF.Runtime.Core;
using REF.Runtime.Online.Service;
using REF.Runtime.Diagnostic;

namespace REF.Runtime.Online.Advertisments.AdMob
{
	public class AdMobAdvertismentService : OnlineService, IAdvertismentService
	{
		[SerializeField] private bool isTest = true;
		[SerializeField] private Placement bannerTestPlacement = new Placement("ca-app-pub-3940256099942544/6300978111", "ca-app-pub-3940256099942544/2934735716", "unexpected_platform");
		[SerializeField] private Placement interstitialTestPlacement = new Placement("ca-app-pub-3940256099942544/1033173712", "ca-app-pub-3940256099942544/4411468910", "unexpected_platform");
		[SerializeField] private Placement rewardedTestPlacement = new Placement("ca-app-pub-3940256099942544/5224354917", "ca-app-pub-3940256099942544/1712485313", "unexpected_platform");

		public void SetTestAds(bool value)
		{
			isTest = value;
		}

		public override void Initialize(System.Action callback)
		{
			GoogleMobileAds.Api.MobileAds.Initialize((status) =>
			{
				base.Initialize(callback);
			});
		}

		public override void Configure(IConfiguration config)
		{
			base.Configure(config);

			var configuration = config as IAdvertismentConfiguration;

			if (configuration == null)
			{
				RefDebug.Error(nameof(AdMobAdvertismentService), $"Config must be of type {nameof(IAdvertismentConfiguration)}!");
				return;
			}

			isTest = configuration.IsTest();
		}

		public IBannerAd CreateBanner(string placement, BannerSettings settings)
		{
			if (isTest)
			{
				placement = bannerTestPlacement.GetPlacement();
			}
			
			return new BannerAd(placement, settings);
		}

		public IInterstitialAd CreateInterstitial(string placement)
		{
			if (isTest)
			{
				placement = interstitialTestPlacement.GetPlacement();
			}

			return new InterstitialAd(placement);
		}

		public IRewardedAd CreateRewarded(string placement)
		{
			if (isTest)
			{
				placement = rewardedTestPlacement.GetPlacement();
			}

			return new RewardedAd(placement);
		}
	}
}

#endif