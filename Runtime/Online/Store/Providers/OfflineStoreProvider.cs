#if REF_STORE && REF_OFFLINE_STORE

using System.Collections.Generic;

using REF.Runtime.Core;
using REF.Runtime.GameSystem.Storage;

namespace REF.Runtime.Online.Store.Providers
{
	public abstract class OfflineStoreProvider<T> : IStoreProvider where T : IItem
	{
		public const string ProviderId = "OfflineStore";

		private IStoreService store;
		private IStoreListener listener;

		private bool initialized = false;
		private List<ITransaction> pendingTransactions = new List<ITransaction>();

		public OfflineStoreProvider(IStoreService store, IStoreListener listener)
		{
			this.store = store;
			this.listener = listener;
		}

		public abstract bool IsOfflineInventoryInitialized();

		public string GetProviderId()
		{
			return ProviderId;
		}

		public void Buy(IProduct product)
		{
			var price = product.GetPrice(ProviderId);

			if (price == null || price.GetPriceType() != PriceType.Item)
			{
				Transaction transaction = new Transaction();
				transaction.SetId("-1");
				transaction.SetFailReason(PurchaseFailReason.ProductUnavailable);
				transaction.SetError("Coudn't find a price of an item for offline store or price is incorrect");
				transaction.SetProviderId(ProviderId);

				listener?.OnPurchaseFailedHandler(transaction);
				return;
			}

			var itemPrice = (IItemPrice)price;
			var priceItemContainer = itemPrice.GetPrice<T>();

			var inventory = GetInventory();

			if (inventory.Contains(priceItemContainer))
			{
				if (listener != null)
				{
					Transaction transaction = new Transaction();
					transaction.SetId(System.Guid.NewGuid().ToString());
					transaction.SetProduct(product);
					transaction.SetProviderId(ProviderId);

					var result = listener.OnPurchaseProcessHandler(transaction);

					if (result != PurchaseProcessResult.Complete)
					{
						transaction.SetFailReason(PurchaseFailReason.ExistingPurchasePending);
						transaction.SetError("Pending purchase!");
						pendingTransactions.Add(transaction);
					}
					else
					{
						inventory.Remove(priceItemContainer);
					}
				}
			}
			else
			{
				Transaction transaction = new Transaction();
				transaction.SetId("-1");
				transaction.SetFailReason(PurchaseFailReason.NotEnoughResources);
				transaction.SetProviderId(ProviderId);
				transaction.SetError("Not enough resource to buy an item!");

				listener?.OnPurchaseFailedHandler(transaction);
			}
		}

		public void Restore()
		{
			foreach (var transaction in pendingTransactions)
			{
				listener?.OnPurchaseProcessHandler(transaction);
			}
		}

		public bool IsInitialized()
		{
			return initialized && IsOfflineInventoryInitialized();
		}

		public bool IsSupported()
		{
			return true;
		}

		public List<ITransaction> GetPendingTransactions()
		{
			return pendingTransactions;
		}

		public void Construct(IApp app)
		{
			
		}

		public void PreInitialize(System.Action callback)
		{
			initialized = false;
			callback?.Invoke();
		}

		public void Configure(IConfiguration config)
		{

		}

		public void Initialize(System.Action callback)
		{
			callback?.Invoke();
		}

		public void PostInitialize(System.Action callback)
		{
			initialized = true;
			callback?.Invoke();
		}

		public void Release(System.Action callback)
		{
			initialized = false;
			callback?.Invoke();
		}

		public void Update()
		{

		}

		public void Suspend()
		{

		}

		public void Resume()
		{

		}

		protected abstract IItemContainer<T> GetInventory();
	}
}

#endif