using UnityEngine;

namespace REF.Runtime.Data
{
	[CreateAssetMenu(fileName = "Transform", menuName = "REF/Data/Variable/Transform")]
	public class TransformVariable : Variable<Transform>
	{
	}

	[System.Serializable]
	public class TransformReference : Reference<Transform, TransformVariable>
	{
	}
}
