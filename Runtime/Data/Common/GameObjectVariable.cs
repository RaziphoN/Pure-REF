using UnityEngine;

namespace REF.Runtime.Data
{
	[CreateAssetMenu(fileName = "GameObject", menuName = "REF/Data/Variable/GameObject")]
	public class GameObjectVariable : Variable<GameObject>
	{
	}

	[System.Serializable]
	public class GameObjectReference : Reference<GameObject, GameObjectVariable>
	{
		public GameObjectReference(GameObject value) : base(value) { }
	}
}
