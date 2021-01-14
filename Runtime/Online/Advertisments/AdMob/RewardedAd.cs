#if REF_ONLINE_ADVERTISMENT && REF_ADMOB_ADVERTISMENT && REF_USE_ADMOB

using GoogleMobileAds.Api;

namespace REF.Runtime.Online.Advertisments.AdMob
{
	public class RewardedAd : IRewardedAd
	{
		public event System.Action OnLoaded;
		public event System.Action OnFailedToLoad;
		public event System.Action OnShow;
		public event System.Action OnHide;
		public event System.Action OnFailedToShow;
		public event System.Action<AdReward> OnReward;

		private AdState state = AdState.Idle;
		private GoogleMobileAds.Api.RewardedAd sdkAd;
		private string placement;

		public RewardedAd(string placement)
		{
			this.placement = placement;	
		}

		public void Dispose()
		{
			DestroyAd();
			state = AdState.Destroyed;
		}

		public AdState GetState()
		{
			return state;
		}

		public string GetPlacement()
		{
			return placement;
		}

		public void Hide()
		{

		}

		public void Load()
		{
			sdkAd = RecreateAd(placement);

			if (state == AdState.Idle && sdkAd != null)
			{
				state = AdState.Loading;
				AdRequest request = new AdRequest.Builder().Build();
				sdkAd.LoadAd(request);
			}
		}

		public void Show()
		{
			if (state == AdState.Loaded)
			{
				state = AdState.Showing;
				sdkAd?.Show();
			}
		}

		private void OnAdHideHandler(object sender, System.EventArgs e)
		{
			state = AdState.Idle;
			OnHide?.Invoke();
		}

		private void OnAdShowHandler(object sender, System.EventArgs e)
		{
			state = AdState.Showing;
			OnShow?.Invoke();
		}

		private void OnAdFailedToLoadHandler(object sender, AdErrorEventArgs e)
		{
			Diagnostic.RefDebug.Log(nameof(BannerAd), $"Failed to load banner: {e.Message}");
			state = AdState.Idle;
			OnFailedToLoad?.Invoke();
		}

		private void OnAdLoadedHandler(object sender, System.EventArgs e)
		{
			state = AdState.Loaded;
			OnLoaded?.Invoke();
		}

		private void OnAdFailedToShowHandler(object sender, AdErrorEventArgs e)
		{
			Diagnostic.RefDebug.Log(nameof(BannerAd), $"Failed to show rewarded: {e.Message}");
			state = AdState.Idle;
			OnFailedToShow?.Invoke();
		}

		private void OnUserEarnedRewardHandler(object sender, Reward reward)
		{
			AdReward adReward = new AdReward(reward.Type, reward.Amount);
			state = AdState.Idle;
			OnReward?.Invoke(adReward);
		}

		private GoogleMobileAds.Api.RewardedAd RecreateAd(string placement)
		{
			DestroyAd();

			this.placement = placement;
			sdkAd = new GoogleMobileAds.Api.RewardedAd(placement);

			if (sdkAd != null)
			{
				sdkAd.OnAdLoaded += OnAdLoadedHandler;
				sdkAd.OnAdFailedToLoad += OnAdFailedToLoadHandler;
				sdkAd.OnAdOpening += OnAdShowHandler;
				sdkAd.OnAdClosed += OnAdHideHandler;
				sdkAd.OnAdFailedToShow += OnAdFailedToShowHandler;
				sdkAd.OnUserEarnedReward += OnUserEarnedRewardHandler;
			}

			return sdkAd;
		}

		private void DestroyAd()
		{
			if (sdkAd != null)
			{
				sdkAd.OnAdLoaded -= OnAdLoadedHandler;
				sdkAd.OnAdFailedToLoad -= OnAdFailedToLoadHandler;
				sdkAd.OnAdOpening -= OnAdShowHandler;
				sdkAd.OnAdClosed -= OnAdHideHandler;
				sdkAd.OnAdFailedToShow -= OnAdFailedToShowHandler;
				sdkAd.OnUserEarnedReward -= OnUserEarnedRewardHandler;
			}

			sdkAd = null;
		}
	}
}

#endif