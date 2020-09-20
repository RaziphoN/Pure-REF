using UnityEngine;

namespace REF.Runtime.Data
{
	[CreateAssetMenu(fileName = "Int", menuName = "REF/Data/Variable/Int")]
	public class IntVariable : Variable<int>
	{
	}

	[System.Serializable]
	public class IntReference : Reference<int, IntVariable>
	{
		public IntReference(int value) : base(value) { }
	}
}
