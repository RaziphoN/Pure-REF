#if REF_ONLINE_ADVERTISMENT && REF_ADMOB_ADVERTISMENT && REF_USE_ADMOB

using GoogleMobileAds.Api;

namespace REF.Runtime.Online.Advertisments.AdMob
{
	public class InterstitialAd : IInterstitialAd
	{
		public event System.Action OnLoaded;
		public event System.Action OnFailedToLoad;
		public event System.Action OnShow;
		public event System.Action OnHide;
		public event System.Action OnLeaveApplication;

		private AdState state = AdState.Idle;
		private GoogleMobileAds.Api.InterstitialAd sdkAd;
		private string placement;

		public InterstitialAd(string placement)
		{
			sdkAd = RecreateAd(placement);
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
			if (state == AdState.Idle)
			{
				state = AdState.Loading;
				AdRequest request = new AdRequest.Builder().Build();
				sdkAd?.LoadAd(request);
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

		private void OnAdLeaveAppHandler(object sender, System.EventArgs e)
		{
			OnLeaveApplication?.Invoke();
		}

		private void OnAdHideHandler(object sender, System.EventArgs e)
		{
			state = AdState.Idle;
			OnHide?.Invoke();
			sdkAd = RecreateAd(placement);
		}

		private void OnAdShowHandler(object sender, System.EventArgs e)
		{
			state = AdState.Showing;
			OnShow?.Invoke();
		}

		private void OnAdFailedToLoadHandler(object sender, AdFailedToLoadEventArgs e)
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

		private GoogleMobileAds.Api.InterstitialAd RecreateAd(string placement)
		{
			DestroyAd();

			this.placement = placement;
			sdkAd = new GoogleMobileAds.Api.InterstitialAd(placement);

			if (sdkAd != null)
			{
				sdkAd.OnAdLoaded += OnAdLoadedHandler;
				sdkAd.OnAdFailedToLoad += OnAdFailedToLoadHandler;
				sdkAd.OnAdOpening += OnAdShowHandler;
				sdkAd.OnAdClosed += OnAdHideHandler;
				sdkAd.OnAdLeavingApplication += OnAdLeaveAppHandler;
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
				sdkAd.OnAdLeavingApplication -= OnAdLeaveAppHandler;
			}

			sdkAd?.Destroy();
			sdkAd = null;
		}
	}
}

#endif