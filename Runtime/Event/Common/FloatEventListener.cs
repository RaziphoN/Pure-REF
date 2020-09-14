using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Framework.EventSystem.Common
{
	[System.Serializable]
	public class FloatUnityEvent : UnityEvent<float> { }

	public class FloatEventListener : EventListener<float>
	{
		[Tooltip("Event to register with.")]
		[SerializeField] private FloatEvent[] eventList = null;

		[Tooltip("Response to invoke when Event is raised.")]
		[SerializeField] private FloatUnityEvent callback = null;

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
