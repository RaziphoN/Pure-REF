using System.Collections.Generic;

namespace Scripts.Framework.EventSystem
{
	public class Event<T> : EventBase
	{
		private List<EventListener<T>> listeners = new List<EventListener<T>>();
		private List<System.Action<T>> callbacks = new List<System.Action<T>>();

		public T param;

		public override void Invoke()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				listeners[i]?.OnInvoke(param);

			for (int i = callbacks.Count - 1; i >= 0; i--)
				callbacks[i]?.Invoke(param);
		}

		public void Subscribe(System.Action<T> action)
		{
			if (!callbacks.Contains(action))
				callbacks.Add(action);
		}

		public void Unsubscribe(System.Action<T> action)
		{
			if (callbacks.Contains(action))
				callbacks.Remove(action);
		}

		public void Subscribe(EventListener<T> listener)
		{
			if (!listeners.Contains(listener))
				listeners.Add(listener);
		}

		public void Unsubscribe(EventListener<T> listener)
		{
			if (listeners.Contains(listener))
				listeners.Remove(listener);
		}
	}
}
