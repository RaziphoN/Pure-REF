namespace REF.Runtime.Online.Network.Response
{
	public interface IResponseHandle
	{
		IResponse Parse(string message);
		void Handle(IResponse response);
	}

	public class ResponseHandle<T> : IResponseHandle where T : IResponse
	{
		private IResponseHandler<T> handler;

		public ResponseHandle(IResponseHandler<T> handler)
		{
			this.handler = handler;
		}

		public IResponse Parse(string data)
		{
			return UnityEngine.JsonUtility.FromJson<T>(data);
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
		void OnResponse(T response);
	}
}
