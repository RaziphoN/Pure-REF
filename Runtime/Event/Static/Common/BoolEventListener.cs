using UnityEngine;
using UnityEngine.Events;

namespace REF.Runtime.EventSystem.Static
{
	[System.Serializable]
	public class BoolUnityEvent : UnityEvent<bool> { }

	public class BoolEventListener : EventListener<bool>
	{
		[Tooltip("Event to register with.")]
		[SerializeField] private BoolEvent[] eventList = null;

		[Tooltip("Response to invoke when Event is raised.")]
		[SerializeField] private BoolUnityEvent callback = null;

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
