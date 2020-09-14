using UnityEngine;
using UnityEngine.EventSystems;

namespace REF.Runtime.UI.Style
{
	// [CreateAssetMenu(fileName = "Style", menuName = "REF/UI/Style/Style")]
	public abstract class StyleObject<T> : ScriptableObject where T : UIBehaviour
	{
		public abstract void Apply(T element);
	}
}
