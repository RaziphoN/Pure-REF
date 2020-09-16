using UnityEngine;
using UnityEngine.Events;

namespace REF.Runtime.EventSystem.Static
{
	[System.Serializable]
	public class StringUnityEvent : UnityEvent<string> { }

	public class StringEventListener : EventListener<string>
	{
		[Tooltip("Event to register with.")]
		[SerializeField] private StringEvent[] eventList = null;

		[Tooltip("Response to invoke when Event is raised.")]
		[SerializeField] public StringUnityEvent callback = null;

		protected override void LinkEvents()
		{
			base.events = eventList;
		}

		protected override void LinkResponse()
		{
			base.response = callback;
		}
	}
}
