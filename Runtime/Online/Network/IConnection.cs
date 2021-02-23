namespace REF.Runtime.Online.Network
{
	public interface IConnection
	{
		event System.Action OnConnect;
		event System.Action OnDisconnect;
		event System.Action<string> OnMessage;

		bool IsConnected();

		void Connect(System.Uri uri);
		void Disconnect();

		void Send(string message);
		void Update(); // loop update
	}

	public interface IConnection<T> : IConnection where T : ISocket
	{
		T GetSocket();
	}
}
