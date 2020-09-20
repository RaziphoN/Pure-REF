using UnityEngine;

using REF.Runtime.UI.Style.Selectable;
using UnityEngine.EventSystems;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Slider")]
	public class Slider : UnityEngine.UI.Slider
	{
		public event System.Action OnUserDragStart;
		public event System.Action<float> OnUserDragEnd;

		[SerializeField] private SliderStyleObject style;
		private bool isControlled = false;

		protected override void Start()
		{
			base.Start();
			style?.Apply(this);
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			isControlled = true;
			OnUserDragStart?.Invoke();
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			isControlled = false;
			OnUserDragEnd?.Invoke(value);
		}

#if UNITY_EDITOR
		protected override void OnValidate()
		{
			base.OnValidate();
			style?.Apply(this);
		}
#endif
	}
}
