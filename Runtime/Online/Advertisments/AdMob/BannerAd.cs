#if REF_ONLINE_ADVERTISMENT && REF_ADMOB_ADVERTISMENT && REF_USE_ADMOB

using GoogleMobileAds.Api;

using REF.Runtime.Diagnostic;

using System.Collections.Generic;

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
			var size = ToSDKSize(settings);
			var pos = ToDipPosition(settings);

			var positionPolicy = settings.GetPositionType();

			if (size == null)
			{
				return;
			}

			this.placement = placement;

			switch (positionPolicy)
			{
				case BannerSettings.PositionPolicy.Defined:
				{
					var relativePos = ToSDKPosition(settings.GetRelativePosition());
					view = new BannerView(placement, size, relativePos);
				}
				break;

				case BannerSettings.PositionPolicy.Custom:
				default:
				{
					view = new BannerView(placement, size, pos.x - (int)(size.Width * 0.5f), pos.y - (int)(size.Height * 0.5f));
				}
				break;
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
				state = AdState.Loaded;
			}
		}

		public void Load()
		{
			// LoadInternal();
		}

		public void Show()
		{
			if (state == AdState.Idle)
			{
				LoadInternal();
			}
			else if (state == AdState.Loaded)
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
			state = AdState.Showing;
			OnLoaded?.Invoke();
		}

		private UnityEngine.Vector2Int ToDipPosition(BannerSettings settings)
		{
			var deviceScale = MobileAds.Utils.GetDeviceScale();
			var pos = settings.GetPosition();

			pos.x = (int)(pos.x / deviceScale);
			pos.y = (int)(pos.y / deviceScale);

			return pos;
		}

		private AdSize ToSDKSize(BannerSettings settings)
		{
			var deviceScale = MobileAds.Utils.GetDeviceScale();
			var size = settings.GetSize();
			
			size.x = (int)(size.x / deviceScale);
			size.y = (int)(size.y / deviceScale);

			var type = settings.GetBannerType();

			switch (type)
			{
				case BannerSettings.Type.Preset:
				{
					/* https://developers.google.com/admob/unity/banner */
					var presets = new List<AdSize>() { AdSize.Banner, AdSize.MediumRectangle, AdSize.IABBanner, AdSize.Leaderboard, new AdSize(320, 100) };
					var closest = FindClosest(size, presets);
					
					return closest;
				}
				break;

				case BannerSettings.Type.Smart:
				{
					return AdSize.SmartBanner;
				}
				break;

				case BannerSettings.Type.Adaptive:
				{
					return AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(size.x);
				}
				break;

				case BannerSettings.Type.Custom:
				{
					return new AdSize(size.x, size.y);
				}
				break;
			}

			return null;
		}

		private AdPosition ToSDKPosition(BannerSettings.Position position)
		{
			switch (position)
			{
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

		private AdSize FindClosest(UnityEngine.Vector2Int target, List<AdSize> presets)
		{
			var diffs = presets.ConvertAll((preset) => { return target - new UnityEngine.Vector2Int(preset.Width, preset.Height); });

			var closest = diffs[0];

			for (int idx = 0; idx < diffs.Count; ++idx)
			{
				var diff = diffs[idx];

				// skip  those that are smaller
				if (diff.x < 0 || diff.y < 0)
				{
					continue;
				}

				var totalPadding = closest.x + closest.y;
				var otherTotalPadding = diff.x + diff.y;

				if (totalPadding > otherTotalPadding)
				{
					closest = diff;
				}
			}

			var index = diffs.IndexOf(closest);
			return presets[index];
		}

		private void LoadInternal()
		{
			if (state == AdState.Idle)
			{
				state = AdState.Loading;
				AdRequest request = new AdRequest.Builder().Build();
				view?.LoadAd(request);
			}
		}
	}
}

#endif