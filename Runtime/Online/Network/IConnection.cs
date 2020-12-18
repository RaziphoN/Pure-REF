namespace REF.Runtime.Online.Network
{
	public interface IConnection<T> where T : ISocket
	{
		event System.Action OnConnect;
		event System.Action OnDisconnect;
		event System.Action<string> OnMessage;

		bool IsConnected();

		T GetSocket();

		void Connect(System.Uri uri);
		void Disconnect();

		void Send(string message);
		void Update(); // loop update
	}
}
