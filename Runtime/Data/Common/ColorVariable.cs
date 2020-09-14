using UnityEngine;

namespace REF.Runtime.Data
{
	[CreateAssetMenu(fileName = "Color", menuName = "REF/Data/Variable/Color")]
	public class ColorVariable : Variable<Color>
	{
	}

	[System.Serializable]
	public class ColorReference : Reference<Color, ColorVariable>
	{
	}
}
