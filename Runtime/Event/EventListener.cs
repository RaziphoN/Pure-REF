using UnityEngine;
using UnityEngine.Events;

using System.Linq;

namespace Scripts.Framework.EventSystem
{
	public abstract class EventListener<T> : MonoBehaviour
	{
		protected Event<T>[] events;
		protected UnityEvent<T> response;

		protected abstract void LinkEvents();
		protected abstract void LinkResponse();

		public void Listen(Event<T> ev)
		{
			if (!events.Contains(ev))
				events = events.Append(ev).ToArray();
		}

		public void StopListen(Event<T> ev)
		{
			if (events.Contains(ev))
				events = events.Where(subscribedEvent => { return subscribedEvent != ev; }).ToArray();
		}

		public void Subscribe(UnityAction<T> method)
		{
			response?.AddListener(method);
		}

		public void Unsubscribe(UnityAction<T> method)
		{
			response?.RemoveListener(method);
		}

		public void UnsubscribeAll()
		{
			response?.RemoveAllListeners();
		}

		public void OnInvoke(T param)
		{
			response?.Invoke(param);
		}

		public void Enable()
		{
			if (events != null)
			{
				foreach (var ev in events)
				{
					ev.Subscribe(this);
				}
			}
		}

		public void Disable()
		{
			if (events != null)
			{
				foreach (var ev in events)
				{
					ev.Unsubscribe(this);
				}
			}
		}

		private void OnEnable()
		{
			LinkEvents();
			LinkResponse();
			Enable();
		}

		private void OnDisable()
		{
			Disable();
		}
	}
}
