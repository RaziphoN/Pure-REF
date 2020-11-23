namespace REF.Runtime.Online.Network
{
	public interface IRequest
	{
		int GetId(); // unique across all requests
		int GetTimeoutTime(); // ms

		string ToNetworkMessage();
	}
}
