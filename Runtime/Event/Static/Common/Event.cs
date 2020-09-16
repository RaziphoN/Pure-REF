using UnityEngine;

using System.Collections.Generic;

namespace REF.Runtime.EventSystem.Static
{
	[CreateAssetMenu(fileName = "Event", menuName = "REF/EventSystem/Event")]
	public class Event : EventBase
	{
		private readonly List<EventListener> listeners = new List<EventListener>();
		private List<System.Action> callbacks = new List<System.Action>();

		public override void Invoke()
		{
			for (int i = listeners.Count - 1; i >= 0; i--)
				listeners[i]?.OnInvoke();

			for (int i = callbacks.Count - 1; i >= 0; i--)
				callbacks[i]?.Invoke();
		}

		public void Subscribe(System.Action action)
		{
			if (!callbacks.Contains(action))
				callbacks.Add(action);
		}

		public void Unsubscribe(System.Action action)
		{
			if (callbacks.Contains(action))
				callbacks.Remove(action);
		}

		public void Subscribe(EventListener listener)
		{
			if (!listeners.Contains(listener))
				listeners.Add(listener);
		}

		public void Unsubscribe(EventListener listener)
		{
			if (listeners.Contains(listener))
				listeners.Remove(listener);
		}
	}
}
