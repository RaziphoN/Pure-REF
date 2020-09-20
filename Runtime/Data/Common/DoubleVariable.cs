using UnityEngine;

namespace REF.Runtime.Data
{
	[CreateAssetMenu(fileName = "Double", menuName = "REF/Data/Variable/Double")]
	public class DoubleVariable : Variable<double>
	{
	}

	[System.Serializable]
	public class DoubleReference : Reference<double, DoubleVariable>
	{
		public DoubleReference(double value) : base(value) { }
	}
}
