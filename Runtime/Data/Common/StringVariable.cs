using UnityEngine;

namespace REF.Runtime.Data
{
	[CreateAssetMenu(fileName = "String", menuName = "REF/Data/Variable/String")]
	public class StringVariable : Variable<string>
	{
	}

	[System.Serializable]
	public class StringReference : Reference<string, StringVariable>
	{
		public StringReference(string value) : base(value) { }
	}
}
