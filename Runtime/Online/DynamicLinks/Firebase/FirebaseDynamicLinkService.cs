#if REF_ONLINE_DYNAMIC_LINK && REF_FIREBASE_DYNAMIC_LINK && REF_USE_FIREBASE

using System.Collections.Generic;

using REF.Runtime.Core;
using REF.Runtime.Diagnostic;
using REF.Runtime.Online.Service;

namespace REF.Runtime.Online.DynamicLinks
{
	public class FirebaseDynamicLinkService : FirebaseService, IDynamicLinkService
	{
		public event System.Action<System.Uri> OnLinkReceived;

		private string linkPrefix = string.Empty;

		public override void Configure(IConfiguration config)
		{
			base.Configure(config);

			var configuration = config as IDynamicLinkConfiguration;

			if (configuration == null)
			{
				RefDebug.Error(nameof(FirebaseDynamicLinkService), $"Config must be of type {nameof(IDynamicLinkConfiguration)}!");
				return;
			}

			linkPrefix = configuration.GetLinkPrefix();
		}

		// immediately returns a link, but all parameters are visible (unsafe) on client
		public System.Uri GetLongLink(DynamicLink longLink)
		{
			return BuildLink(longLink);
		}

		public void RequestShortLink(DynamicLink longLink, System.Action<System.Uri> onLinkCreated, System.Action onRequestFailed = null)
		{
			if (!FirebaseInitializer.AllowApiCalls || !IsInitialized())
			{
				onRequestFailed?.Invoke();
				return;
			}

			// Firebase doesn't support deep link with query parameters in it's component class. We have to manually escape deep link before short link request
			var link = BuildLink(longLink);

			if (link != null)
			{
				InternalRequest(link, onLinkCreated, onRequestFailed);
			}
			else
			{
				onRequestFailed();
			}
		}

		public override void Release(System.Action callback)
		{
			if (IsInitialized())
			{
				Firebase.DynamicLinks.DynamicLinks.DynamicLinkReceived -= OnDynamicLinkReceivedHandler;
			}

			base.Release(callback);
		}

		protected override void FinalizeInit(bool successful, System.Action callback)
		{
			if (successful)
			{
				Firebase.DynamicLinks.DynamicLinks.DynamicLinkReceived += OnDynamicLinkReceivedHandler;
			}

			callback?.Invoke();
		}

		private System.Uri BuildDeepLink(string baseUri, IDictionary<string, string> data)
		{
			Utilities.UriBuilder builder = new Utilities.UriBuilder();
			builder.SetUrl(baseUri);

			if (data != null)
			{
				foreach (var component in data)
					builder.AddParameter(component.Key, component.Value);
			}

			System.UriBuilder uriBuilder = new System.UriBuilder();

			return new System.Uri(builder.ToString());
		}

		private System.Uri BuildLink(DynamicLink longLink)
		{
			System.Uri link = BuildDeepLink(longLink.BaseUri, longLink.Data);

			Firebase.DynamicLinks.DynamicLinkComponents components = new Firebase.DynamicLinks.DynamicLinkComponents(link, linkPrefix);


			if (longLink.AndroidParams != null)
			{
				Firebase.DynamicLinks.AndroidParameters androidParams = new Firebase.DynamicLinks.AndroidParameters(longLink.AndroidParams.PackageName);

				if (!string.IsNullOrEmpty(longLink.AndroidParams.FallbackUri))
					androidParams.FallbackUrl = new System.Uri(longLink.AndroidParams.FallbackUri);

				components.AndroidParameters = androidParams;
			}

			if (longLink.IosParams != null)
			{
				Firebase.DynamicLinks.IOSParameters iosParams = new Firebase.DynamicLinks.IOSParameters(longLink.IosParams.BundleId);

				if (!string.IsNullOrEmpty(longLink.IosParams.AppStoreId))
					iosParams.AppStoreId = longLink.IosParams.AppStoreId;

				if (!string.IsNullOrEmpty(longLink.IosParams.CustomScheme))
					iosParams.CustomScheme = longLink.IosParams.CustomScheme;

				if (!string.IsNullOrEmpty(longLink.IosParams.FallbackUri))
					iosParams.FallbackUrl = new System.Uri(longLink.IosParams.FallbackUri);

				if (!string.IsNullOrEmpty(longLink.IosParams.IPadBundleId))
					iosParams.IPadBundleId = longLink.IosParams.IPadBundleId;

				if (!string.IsNullOrEmpty(longLink.IosParams.IPadFallbackUri))
					iosParams.IPadFallbackUrl = new System.Uri(longLink.IosParams.IPadFallbackUri);

				components.IOSParameters = iosParams;
			}

			if (longLink.Social != null)
			{
				Firebase.DynamicLinks.SocialMetaTagParameters socialParams = new Firebase.DynamicLinks.SocialMetaTagParameters();

				socialParams.Title = longLink.Social.Title;
				socialParams.Description = longLink.Social.Description;

				if (!string.IsNullOrEmpty(longLink.Social.ImageUrl))
					socialParams.ImageUrl = new System.Uri(longLink.Social.ImageUrl);

				components.SocialMetaTagParameters = socialParams;
			}

			// if LongDynamicLink is null here, it means that our PreferedLinkPrefix empty or invalid
			if (components.LongDynamicLink == null)
				return null;

			var internalLongDynamicLink = components.LongDynamicLink.OriginalString;
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(internalLongDynamicLink);

			// escape url's used as parameters for dynamic link. Firebase doesn't escape them, so query parameters may lost in default implementation
			{
				var deepLink = components.Link.OriginalString;
				stringBuilder.Replace(deepLink, System.Uri.EscapeDataString(deepLink));

				// android fallback url
				if (components.AndroidParameters != null && components.AndroidParameters.FallbackUrl != null)
				{
					var fallbackUrl = components.AndroidParameters.FallbackUrl.OriginalString;
					stringBuilder.Replace(fallbackUrl, System.Uri.EscapeDataString(fallbackUrl));
				}

				if (components.IOSParameters != null)
				{
					// ios fallback url
					if (components.IOSParameters.FallbackUrl != null)
					{
						var fallbackUrl = components.IOSParameters.FallbackUrl.OriginalString;
						stringBuilder.Replace(fallbackUrl, System.Uri.EscapeDataString(fallbackUrl));
					}

					// ios IPad fallback url
					if (components.IOSParameters.IPadFallbackUrl != null)
					{
						var fallbackUrl = components.IOSParameters.IPadFallbackUrl.OriginalString;
						stringBuilder.Replace(fallbackUrl, System.Uri.EscapeDataString(fallbackUrl));
					}
				}
			}

			return new System.Uri(stringBuilder.ToString());
		}

		private void InternalRequest(System.Uri longDynamicLink, System.Action<System.Uri> onLinkCreated, System.Action onRequestFailed)
		{
			Firebase.DynamicLinks.DynamicLinkOptions options = new Firebase.DynamicLinks.DynamicLinkOptions();

			Firebase.DynamicLinks.DynamicLinks.GetShortLinkAsync(longDynamicLink, options).ContinueWith(task =>
			{
				if (!task.IsCompleted)
				{
					onRequestFailed?.Invoke();
					return;
				}

				Firebase.DynamicLinks.ShortDynamicLink shortLink = task.Result;
				onLinkCreated?.Invoke(shortLink.Url);
			});
		}

		private void OnDynamicLinkReceivedHandler(object sender, Firebase.DynamicLinks.ReceivedDynamicLinkEventArgs e)
		{
			OnLinkReceived?.Invoke(e.ReceivedDynamicLink.Url);
		}
	}
}

#endif