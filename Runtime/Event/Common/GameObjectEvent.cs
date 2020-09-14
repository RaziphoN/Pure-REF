using UnityEngine;

namespace Scripts.Framework.EventSystem.Common
{
	[CreateAssetMenu(fileName = "GameObjectEvent", menuName = "REF/EventSystem/GameObjectEvent")]
	public class GameObjectEvent : Event<GameObject> { }
}
