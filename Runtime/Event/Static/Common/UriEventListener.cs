using UnityEngine;
using UnityEngine.Events;

namespace REF.Runtime.EventSystem.Static
{
	[System.Serializable]
	public class UriUnityEvent : UnityEvent<System.Uri> { }

	public class UriEventListener : EventListener<System.Uri>
	{
		[Tooltip("Event to register with.")]
		[SerializeField] private UriEvent[] eventList = null;

		[Tooltip("Response to invoke when Event is raised.")]
		[SerializeField] private UriUnityEvent callback = null;

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
