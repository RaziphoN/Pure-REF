namespace REF.Runtime.Online.Network.Response
{
	public interface IResponse
	{
		bool IsCompleted();
		bool IsFaulted();
		bool IsCanceled();
		bool IsTimeout();

		string GetError();
		string GetRaw();
	}
}
