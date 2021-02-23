namespace REF.Runtime.Online.Network
{
	public interface ISocketConfiguration
	{
		long GetTimeoutThresholdMilliseconds();
		long GetPingIntervalMilliseconds();
	}

	public interface ISocket
	{
		event System.Action OnConnect;
		event System.Action OnDisconnect;
		event System.Action<string> OnMessage;

		bool IsConnected();

		void Init(ISocketConfiguration config);
		void Connect(System.Uri uri);
		void Disconnect();

		void Send(string message);
	}
}
