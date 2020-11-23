using System.Threading;
using System.Collections.Generic;

namespace REF.Runtime.Online.Network
{
	public class SyncConnection<T> where T : ISocket, new()
	{
		public event System.Action OnConnect;
		public event System.Action OnDisconnect;
		public event System.Action<string> OnResponse;

		private Connection<T> connection = new Connection<T>();

		private object sender = new object();
		private Thread senderThread;
		private Queue<string> senderQueue = new Queue<string>();

		private object receiver = new object();
		private Queue<string> receiverQueue = new Queue<string>();

		public SyncConnection()
		{
			connection.OnConnect += OnConnectHandler;
			connection.OnDisconnect += OnDisconnectHandler;
			connection.OnMessage += OnResponseHandler;
		}

		~SyncConnection()
		{
			connection.OnConnect -= OnConnectHandler;
			connection.OnDisconnect -= OnDisconnectHandler;
			connection.OnMessage -= OnResponseHandler;

			connection.Disconnect();
		}

		public T GetSocket()
		{
			return connection.GetSocket();
		}

		public bool IsConnected()
		{
			return connection.IsConnected();
		}

		public void Connect(System.Uri uri)
		{
			if (connection.IsConnected())
			{
				return;
			}

			connection.Connect(uri);
		}

		public void Disconnect()
		{
			if (!connection.IsConnected())
			{
				return;
			}

			connection.Disconnect();
		}

		public void Send(string message)
		{
			lock (sender)
			{
				senderQueue.Enqueue(message);
			}
		}

		// call from the thread on which you want to syncronize response callback
		public void Update()
		{
			Receiver();
		}

		private void OnResponseHandler(string response)
		{
			lock (receiver)
			{
				receiverQueue.Enqueue(response);
			}
		}

		private void OnConnectHandler()
		{
			Start(ref senderThread, Sender);
			// Start(ref receiverThread, Receiver);

			OnConnect?.Invoke();
		}

		private void OnDisconnectHandler()
		{
			if (senderThread != null)
			{
				Stop(senderThread);
				senderThread = null;
			}

			//if (receiverThread != null)
			//{
			//	Stop(receiverThread);
			//	receiverThread = null;
			//}

			OnDisconnect?.Invoke();
		}

		private void Start(ref Thread thread, ThreadStart main)
		{
			if (thread != null)
			{
				thread.Abort();
				thread = null;
			}

			thread = new Thread(main);
			thread.Start();
		}

		private void Stop(Thread thread)
		{
			thread.Abort();
		}

		private void Sender()
		{
			while (true)
			{
				lock (sender)
				{
					while (senderQueue.Count > 0)
					{
						var message = senderQueue.Dequeue();
						connection.Send(message);
					}
				}
			}
		}

		private void Receiver()
		{
			lock (receiver)
			{
				while (receiverQueue.Count > 0)
				{
					var response = receiverQueue.Dequeue();
					OnResponse?.Invoke(response);
				}
			}
		}
	}
}
