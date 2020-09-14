using UnityEngine;

namespace REF.Runtime.Data
{
	// Example
	// [CreateAssetMenu(fileName = "Variable", menuName = "REF/Data/Variable/Variable")]
	public abstract class Variable<T> : ScriptableObject
	{
		[SerializeField, TextArea(7, 7)] private string Description;
		[Space(10)]
		public T Value;
	}
}
