namespace REF.Runtime.Online.Network.Response
{
	public interface IResponseHandle
	{
		IResponse Create(string message);
		void Handle(IResponse response);
	}

	public class ResponseHandle<T> : IResponseHandle where T : IResponse, new()
	{
		private IResponseHandler<T> handler;

		public ResponseHandle(IResponseHandler<T> handler)
		{
			this.handler = handler;
		}

		public IResponse Create()
		{
			return new T();
		}

		public IResponse Create(string message)
		{
			return handler.Create(message);
		}

		public void Handle(IResponse response)
		{
			if (!(response is T))
			{
				return;
			}

			handler?.OnResponse((T)response);
		}
	}

	public interface IResponseHandler<T> where T : IResponse
	{
		T Create(string message);
		void OnResponse(T response);
	}
}
