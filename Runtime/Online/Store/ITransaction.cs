#if REF_STORE

namespace REF.Runtime.Online.Store
{
	public interface ITransaction
	{
		bool IsSuccessfull();
		bool IsError();

		IProduct GetProduct();

		string GetProviderId();
		string GetId();
		string GetReceipt();
		string GetError();
		PurchaseFailReason GetFailReason();
	}
}

#endif
