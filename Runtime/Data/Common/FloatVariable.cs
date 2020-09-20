using UnityEngine;

namespace REF.Runtime.Data
{
	[CreateAssetMenu(fileName = "Float", menuName = "REF/Data/Variable/Float")]
	public class FloatVariable : Variable<float>
	{
	}

	[System.Serializable]
	public class FloatReference : Reference<float, FloatVariable>
	{
		public FloatReference(float value) : base(value) { }
	}
}
