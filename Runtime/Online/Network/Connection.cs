using System;

namespace REF.Runtime.Online.Network
{
	public class Connection<T> : ISocket where T : ISocket, new()
	{
		public event Action OnConnect;
		public event Action OnDisconnect;
		public event Action<string> OnMessage;

		private T socket;

		public Connection()
		{
			socket = new T();
		}

		public T GetSocket()
		{
			return socket;
		}

		public void Connect(Uri uri)
		{
			if (IsConnected())
			{
				return;
			}

			socket = new T();

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
