using UnityEngine;

namespace REF.Runtime.Data
{
	[CreateAssetMenu(fileName = "Bool", menuName = "REF/Data/Variable/Bool")]
	public class BoolVariable : Variable<bool>
	{
	}

	[System.Serializable]
	public class BoolReference : Reference<bool, BoolVariable>
	{
		public BoolReference(bool value) : base(value) { }
	}
}
