using UnityEngine;
using UnityEngine.Events;

namespace REF.Runtime.EventSystem.Static
{
	[System.Serializable]
	public class GameObjectUnityEvent : UnityEvent<GameObject> { }

	public class GameObjectEventListener : EventListener<GameObject>
	{
		[Tooltip("Event to register with.")]
		[SerializeField] private GameObjectEvent[] eventList = null;

		[Tooltip("Response to invoke when Event is raised.")]
		[SerializeField] private GameObjectUnityEvent callback = null;

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
