using UnityEngine;
using UnityEngine.Events;

namespace REF.Runtime.EventSystem.Static
{
	public class EventListener : MonoBehaviour
	{
		[SerializeField] protected Event[] events;
		[SerializeField] protected UnityEvent response;

		public void Subscribe(UnityAction method)
		{
			response?.AddListener(method);
		}

		public void Unsubscribe(UnityAction method)
		{
			response?.RemoveListener(method);
		}

		public void UnsubscribeAll()
		{
			response?.RemoveAllListeners();
		}

		public void OnInvoke()
		{
			response?.Invoke();
		}

		public void Enable()
		{
			if (events != null)
			{
				foreach (var ev in events)
					ev.Subscribe(this);
			}
		}

		public void Disable()
		{
			if (events != null)
			{
				foreach (var ev in events)
					ev.Unsubscribe(this);
			}
		}

		private void OnEnable()
		{
			Enable();
		}

		private void OnDisable()
		{
			Disable();
		}
	}
}
