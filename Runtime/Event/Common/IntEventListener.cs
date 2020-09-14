using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Framework.EventSystem.Common
{
	[System.Serializable]
	public class IntUnityEvent : UnityEvent<int> { }

	public class IntEventListener : EventListener<int>
	{
		[Tooltip("Event to register with.")]
		[SerializeField] private IntEvent[] eventList = null;

		[Tooltip("Response to invoke when Event is raised.")]
		[SerializeField] private IntUnityEvent callback = null;

		protected override void LinkEvents()
		{
			events = eventList;
		}

		protected override void LinkResponse()
		{
			response = callback;
		}
	}
}
