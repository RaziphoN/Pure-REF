#if REF_ONLINE_ADVERTISMENT && REF_ADMOB_ADVERTISMENT && REF_USE_ADMOB

using GoogleMobileAds.Api;

namespace REF.Runtime.Online.Advertisments.AdMob
{
	public class BannerAd : IBannerAd
	{
		public event System.Action OnLoaded;
		public event System.Action OnFailedToLoad;
		public event System.Action OnShow;
		public event System.Action OnHide;
		public event System.Action OnLeaveApplication;

		private AdState state = AdState.Idle;
		private BannerView view;
		private string placement;

		public BannerAd(string placement, BannerSettings settings)
		{
			var size = ToSDKSize(settings.GetSize());

			if (size == null)
			{
				return;
			}

			this.placement = placement;
			
			if (settings.IsRelativePosition())
			{
				var relativePos = ToSDKPosition(settings.GetRelativePosition());
				view = new BannerView(placement, size, relativePos);
			}
			else
			{
				var pos = settings.GetScreenPosition();
				view = new BannerView(placement, size, pos.x, pos.y);
			}

			if (view != null)
			{
				view.OnAdLoaded += OnAdLoadedHandler;
				view.OnAdFailedToLoad += OnAdFailedToLoadHandler;
				view.OnAdOpening += OnAdShowHandler;
				view.OnAdClosed += OnAdHideHandler;
				view.OnAdLeavingApplication += OnAdLeaveAppHandler;
			}
		}

		public void Dispose()
		{
			if (view != null)
			{
				view.OnAdLoaded -= OnAdLoadedHandler;
				view.OnAdFailedToLoad -= OnAdFailedToLoadHandler;
				view.OnAdOpening -= OnAdShowHandler;
				view.OnAdClosed -= OnAdHideHandler;
				view.OnAdLeavingApplication -= OnAdLeaveAppHandler;
			}

			view?.Destroy();
			view = null;
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
			if (state == AdState.Showing)
			{
				view?.Hide();
				state = AdState.Idle;
			}
		}

		public void Load()
		{
			if (state == AdState.Idle)
			{
				state = AdState.Loading;
				AdRequest request = new AdRequest.Builder().Build();
				view?.LoadAd(request);
			}
		}

		public void Show()
		{
			if (state == AdState.Loaded)
			{
				state = AdState.Showing;
				view?.Show();
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

		private AdSize ToSDKSize(BannerSettings.Size size)
		{
			if (size.Height == 0 && size.Width != 0)
			{
				switch (size.Orientation)
				{
					case BannerSettings.Orientation.Current:
					{
						return AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(size.Width);
					}
					break;

					case BannerSettings.Orientation.Landscape:
					{
						return AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(size.Width);
					}
					break;

					case BannerSettings.Orientation.Portrait:
					{
						return AdSize.GetPortraitAnchoredAdaptiveBannerAdSizeWithWidth(size.Width);
					}
					break;
				}
			}
			else if (size.Height == 0 && size.Width == 0)
			{
				return AdSize.SmartBanner;
			}
			else
			{
				return new AdSize(size.Width, size.Height);
			}

			return null;
		}

		private AdPosition ToSDKPosition(BannerSettings.Position position)
		{
			switch (position)
			{
				case BannerSettings.Position.Undefined:
				{
					return 0;
				}
				break;

				case BannerSettings.Position.Top:
				{
					return AdPosition.Top;
				}
				break;

				case BannerSettings.Position.Bottom:
				{
					return AdPosition.Bottom;
				}
				break;

				case BannerSettings.Position.TopLeft:
				{
					return AdPosition.TopLeft;
				}
				break;

				case BannerSettings.Position.TopRight:
				{
					return AdPosition.TopRight;
				}
				break;

				case BannerSettings.Position.BottomLeft:
				{
					return AdPosition.BottomLeft;
				}
				break;

				case BannerSettings.Position.BottomRight:
				{
					return AdPosition.BottomRight;
				}
				break;

				case BannerSettings.Position.Center:
				{
					return AdPosition.Center;
				}
				break;
			}

			return 0;
		}
	}
}

#endif