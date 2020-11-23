using UnityEngine;

using System.Collections.Generic;

namespace REF.Runtime.Online.Network
{
	public class Client<TSocket> where TSocket : ISocket, new()
	{
		private SyncConnection<TSocket> connection = new SyncConnection<TSocket>();

		private Queue<IRequest> requests = new Queue<IRequest>();

		public void Send(IRequest request)
		{

		}


		public void Update()
		{

		}
	}
}
