namespace REF.Runtime.Online.Network
{
	public class Connection<T> : IConnection<T> where T : ISocket, new()
	{
		public event System.Action OnConnect;
		public event System.Action OnDisconnect;
		public event System.Action<string> OnMessage;

		private T socket;

		public Connection(ISocketConfiguration socketConfig)
		{
			socket = new T(); // this is bad, but i don't know how to traverse socket config to the socket
			socket.Init(socketConfig);
		}

		public T GetSocket()
		{
			return socket;
		}

		public void Connect(System.Uri uri)
		{
			if (IsConnected())
			{
				return;
			}

			socket.OnConnect += OnSocketConnectHandler;
			socket.OnDisconnect += OnSocketDisconnectHandler;
			socket.OnMessage += OnSocketMessageReceivedHandler;

			socket.Connect(uri);
		}

		public void Disconnect()
		{
			if (IsConnected())
			{
				socket.OnConnect -= OnSocketConnectHandler;
				socket.OnDisconnect -= OnSocketDisconnectHandler;
				socket.OnMessage -= OnSocketMessageReceivedHandler;

				socket?.Disconnect();
				socket = default(T);
				OnSocketDisconnectHandler();
			}
		}

		public bool IsConnected()
		{
			if (socket == null)
			{
				return false;
			}

			return socket.IsConnected();
		}

		public void Send(string message)
		{
			if (!IsConnected())
			{
				return;
			}

			socket.Send(message);
		}

		public void Update()
		{

		}

		private void OnSocketMessageReceivedHandler(string message)
		{
			OnMessage?.Invoke(message);
		}

		private void OnSocketConnectHandler()
		{
			OnConnect?.Invoke();
		}

		private void OnSocketDisconnectHandler()
		{
			OnDisconnect?.Invoke();
		}
	}
}
