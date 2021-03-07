#if REF_STORE

namespace REF.Runtime.Online.Store
{
	public enum PurchaseProcessResult
	{
		Pending,
		Complete
	}

	public enum PurchaseFailReason
	{
		PurchasingUnavailable,
		ExistingPurchasePending,
		ProductUnavailable,
		SignatureInvalid,
		UserCancelled,
		PaymentDeclined,
		DuplicateTransaction,
		NotEnoughResources,
		Unknown
	}

	public interface IStoreListener
	{
		PurchaseProcessResult OnPurchaseProcessHandler(ITransaction transaction);
		void OnPurchaseFailedHandler(ITransaction transaction);
		void OnRestoreHandler(bool isOk);
	}
}

#endif