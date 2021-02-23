using UnityEngine;
using UnityEngine.Purchasing;

using REF.Runtime.Core;
using REF.Runtime.Diagnostic;

namespace REF.Runtime.Online.Store.Providers
{
	public class UnityStoreProvider : IStoreProvider, UnityEngine.Purchasing.IStoreListener
	{
		public const string ProviderId = "UnityStore";

		private bool initialized = false;
		private System.Action postponedCallback;
		private InitializationFailureReason initFailReason;

		private IStoreController controller;
		private IExtensionProvider extensionProvider;

		private IStoreService store;
		private IStoreListener listener;

		public UnityStoreProvider(IStoreService store, IStoreListener listener)
		{
			this.store = store;
			this.listener = listener;
		}

		public string GetProviderId()
		{
			return ProviderId;
		}

		public bool IsSupported()
		{
			return true; // TODO: Check UNITY IAP documentation on that
		}

		public bool IsInitialized()
		{
			return initialized;
		}

		public IStoreController GetController()
		{
			return controller;
		}

		public IExtensionProvider GetExtensionProvider()
		{
			return extensionProvider;
		}

		public void Buy(IProduct product)
		{
			if (!IsInitializedInternal())
			{
				Transaction transaction = new Transaction();
				transaction.SetId("-1");
				transaction.SetFailReason(PurchaseFailReason.Unknown);
				transaction.SetError("Coudn't buy product, because provider is not initialized yet!");
				transaction.SetProduct(product);
				transaction.SetProviderId(ProviderId);

				listener?.OnPurchaseFailedHandler(transaction);
			}

			var id = product.GetId();
			var unityStoreProduct = controller.products.WithID(id);

			if (unityStoreProduct == null)
			{
				Transaction transaction = new Transaction();
				transaction.SetId("-1");
				transaction.SetFailReason(PurchaseFailReason.ProductUnavailable);
				transaction.SetError($"Coudn't buy product, because there is no product with such store id in Unity IAP: {id}");
				transaction.SetProduct(product);
				transaction.SetProviderId(ProviderId);

				listener?.OnPurchaseFailedHandler(transaction);
			}

			controller?.InitiatePurchase(unityStoreProduct);
		}

		public void Restore()
		{
			if (!IsInitializedInternal())
			{
				OnRestoreHandler(false);
				return;
			}

			if (Application.platform == RuntimePlatform.WSAPlayerX86 || Application.platform == RuntimePlatform.WSAPlayerX64 || Application.platform == RuntimePlatform.WSAPlayerARM)
			{
				extensionProvider.GetExtension<IMicrosoftExtensions>().RestoreTransactions();
				OnRestoreHandler(true);
			}
			else if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.tvOS)
			{
				extensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(OnRestoreHandler);
			}
			else if (Application.platform == RuntimePlatform.Android && StandardPurchasingModule.Instance().appStore == AppStore.SamsungApps)
			{
				extensionProvider.GetExtension<ISamsungAppsExtensions>().RestoreTransactions(OnRestoreHandler);
			}
			else
			{
				OnRestoreHandler(false);
			}
		}

		public void Construct(IApp app)
		{

		}

		public void PreInitialize(System.Action callback)
		{
			callback?.Invoke();
		}

		public void Configure(IConfiguration config)
		{
			var configuration = config as IStoreConfiguration;

			if (configuration == null)
			{
				RefDebug.Error(nameof(UnityStoreProvider), $"Config must be of type {nameof(IStoreConfiguration)}!");
				return;
			}
		}

		public void Initialize(System.Action callback)
		{
			RefDebug.Log(nameof(UnityStoreProvider), "Store initializing...");

			var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

			var productCount = store.GetProductCount();

			for (int idx = 0; idx < productCount; ++idx)
			{
				var product = store.GetProductByIdx(idx);

				if (!product.IsDefined(ProviderId))
				{
					continue;
				}

				var id = product.GetId();
				var type = product.GetProductType();
				var storeIds = product.GetStoreIds(ProviderId);

				IDs ids = new IDs();

				foreach (var storeId in storeIds)
				{
					ids.Add(storeId.Value, storeId.Key);
				}

				builder.AddProduct(id, FromRefEnum(type), ids);
			}

#if UNITY_IOS
            // var configuration = builder.Configure<IAppleConfiguration>();
            // configuration.SetApplePromotionalPurchaseInterceptorCallback(OnProductPurchasedHandler);
#endif

#if UNITY_ANDROID
			// var configuration = builder.Configure<IGooglePlayConfiguration>();
#endif

			UnityPurchasing.Initialize(this, builder);
			callback?.Invoke();
		}

		public void PostInitialize(System.Action callback)
		{
			callback?.Invoke();
		}

		public void Update()
		{

		}

		public void Release(System.Action callback)
		{
			initialized = false;
			postponedCallback = null;
			store = null;
			listener = null;
			controller = null;
			extensionProvider = null;

			callback?.Invoke();
		}

		public void Suspend()
		{

		}

		public void Resume()
		{

		}

		public void OnInitialized(IStoreController controller, IExtensionProvider extensionProvider)
		{
			RefDebug.Log(nameof(UnityStoreProvider), "Store initialized successfully");

			initialized = true;
			this.controller = controller;
			this.extensionProvider = extensionProvider;

			var products = controller.products.all;

			for (int idx = 0; idx < products.Length; ++idx)
			{
				var unityProduct = products[idx];
				var id = unityProduct.definition.id;
				var product = store.GetProductById(id);

				if (product != null)
				{
					product.SetEnabled(ProviderId, unityProduct.definition.enabled);
					product.SetAvailable(ProviderId, unityProduct.availableToPurchase);
					
					var meta = product.GetMeta(ProviderId);
					if (meta != null)
					{
						meta.SetLocalizedTitle(unityProduct.metadata.localizedTitle);
						meta.SetLocalizedDescription(unityProduct.metadata.localizedDescription);
					}

					var price = product.GetPrice(ProviderId) as ICurrencyPrice;
					if (price != null)
					{
						price.SetCurrencyCode(unityProduct.metadata.isoCurrencyCode);
						price.SetLocalizedPrice(unityProduct.metadata.localizedPrice);
						price.SetLocalizedPriceString(unityProduct.metadata.localizedPriceString);
					}

					OnPostConvertProduct(product, unityProduct);
				}
			}
		}

		public void OnInitializeFailed(InitializationFailureReason reason)
		{
			RefDebug.Log(nameof(UnityStoreProvider), "Store failed to initialize");

			initFailReason = reason;
			initialized = true;
			controller = null;
			extensionProvider = null;
		}

		public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
		{
			var product = args.purchasedProduct;
			var productId = product.definition.id;
			var storeProduct = store.GetProductById(productId);

			Transaction transaction = new Transaction();
			transaction.SetId(product.transactionID);
			transaction.SetProduct(storeProduct);
			transaction.SetProviderId(ProviderId);

			if (product.hasReceipt)
			{
				transaction.SetReceipt(product.receipt);
			}

			if (listener != null)
			{
				var result = listener.OnPurchaseProcessHandler(transaction);
				return FromRefEnum(result);
			}

			return PurchaseProcessingResult.Pending;
		}

		public void OnPurchaseFailed(Product product, PurchaseFailureReason reason)
		{
			var productId = product.definition.id;
			var storeProduct = store.GetProductById(productId);

			Transaction transaction = new Transaction();
			transaction.SetId(product.transactionID);
			transaction.SetProduct(storeProduct);
			transaction.SetFailReason(ToRefEnum(reason));
			transaction.SetProviderId(ProviderId);
			transaction.SetError($"Coudn't buy product, because of reason: {transaction.GetFailReason()}");

			listener?.OnPurchaseFailedHandler(transaction);
		}

		public virtual void OnPostConvertProduct(IProduct product, Product unityProduct)
		{

		}

		private void OnRestoreHandler(bool isOk)
		{
			listener?.OnRestoreHandler(isOk);
		}

		private bool IsInitializedInternal()
		{
			return controller != null;
		}

		private PurchaseProcessingResult FromRefEnum(PurchaseProcessResult result)
		{
			switch (result)
			{
				case PurchaseProcessResult.Pending:
				{
					return PurchaseProcessingResult.Pending;
				}
				break;

				case PurchaseProcessResult.Complete:
				{
					return PurchaseProcessingResult.Complete;
				}
				break;

				default:
				{
					return PurchaseProcessingResult.Complete;
				}
				break;
			}
		}

		private UnityEngine.Purchasing.ProductType FromRefEnum(ProductType type)
		{
			switch (type)
			{
				case ProductType.Consumable:
				{
					return UnityEngine.Purchasing.ProductType.Consumable;
				}
				break;

				case ProductType.NonConsumable:
				{
					return UnityEngine.Purchasing.ProductType.NonConsumable;
				}
				break;

				case ProductType.Subscription:
				{
					return UnityEngine.Purchasing.ProductType.Subscription;
				}
				break;
			}

			return UnityEngine.Purchasing.ProductType.NonConsumable;
		}

		private PurchaseFailReason ToRefEnum(PurchaseFailureReason reason)
		{
			switch (reason)
			{
				case PurchaseFailureReason.PurchasingUnavailable:
				{
					return PurchaseFailReason.PurchasingUnavailable;
				}
				break;

				case PurchaseFailureReason.ExistingPurchasePending:
				{
					return PurchaseFailReason.ExistingPurchasePending;
				}
				break;

				case PurchaseFailureReason.ProductUnavailable:
				{
					return PurchaseFailReason.ProductUnavailable;
				}
				break;

				case PurchaseFailureReason.SignatureInvalid:
				{
					return PurchaseFailReason.SignatureInvalid;
				}
				break;

				case PurchaseFailureReason.UserCancelled:
				{
					return PurchaseFailReason.UserCancelled;
				}
				break;

				case PurchaseFailureReason.PaymentDeclined:
				{
					return PurchaseFailReason.PaymentDeclined;
				}
				break;

				case PurchaseFailureReason.DuplicateTransaction:
				{
					return PurchaseFailReason.DuplicateTransaction;
				}
				break;

				case PurchaseFailureReason.Unknown:
				default:
				{
					return PurchaseFailReason.Unknown;
				}
				break;
			}
		}
	}
}
