using UnityEngine;

namespace REF.Runtime.Data
{
	[CreateAssetMenu(fileName = "RectTransform", menuName = "REF/Data/Variable/RectTransform")]
	public class RectTransformVariable : Variable<RectTransform>
	{
	}

	[System.Serializable]
	public class RectTransformReference : Reference<RectTransform, RectTransformVariable>
	{
	}
}
