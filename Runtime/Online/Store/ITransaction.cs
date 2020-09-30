namespace REF.Runtime.Online.Store
{
	public interface ITransaction
	{
		bool IsSuccessfull();
		bool HasError();

		IProduct GetProduct();

		string GetId();
		string GetReceipt();
		string GetError();
	}
}
