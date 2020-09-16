namespace REF.Runtime.EventSystem.Bus
{
	public class Subscription<T> : ISubscription where T : IEventPayload 
	{
		private string id;
		private System.Action<T> callback;

		public Subscription(System.Action<T> callback, string id)
		{
			this.callback = callback;
			this.id = id;
		}

		public string GetId()
		{
			return id;
		}

		public void Raise(IEventPayload ev)
		{
			if (ev is T)
			{
				var tEvent = (T)ev;
				callback?.Invoke(tEvent);
			}
		}

		public bool Same(Subscription<T> other)
		{
			return this.callback == other.callback;
		}
	}
}
