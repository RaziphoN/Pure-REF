using UnityEngine;

namespace REF.Runtime.UI.Style.Selectable
{
	[System.Serializable]
	public class ScrollRectStyle : Style<UnityEngine.UI.ScrollRect>, IScrollRectStyle
	{
		[SerializeField] private bool horizontal = true;
		[SerializeField] private UnityEngine.UI.ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility = UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
		[SerializeField] private float horizontalSpacing = -3f;

		[SerializeField] private bool vertical = true;
		[SerializeField] private UnityEngine.UI.ScrollRect.ScrollbarVisibility verticalScrollbarVisibility = UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
		[SerializeField] private float verticalSpacing = -3f;

		[SerializeField] private UnityEngine.UI.ScrollRect.MovementType movementType = UnityEngine.UI.ScrollRect.MovementType.Elastic;
		[SerializeField] private float elasticity = 0.1f;

		[SerializeField] private bool inertia = true;
		[SerializeField] private float decelerationRate = 0.135f;
		[SerializeField] private float scrollSensivity = 1f;

		public override void Apply(UnityEngine.UI.ScrollRect element)
		{
			element.horizontal = UseHorizontal();
			element.horizontalScrollbarVisibility = GetHorizontalScrollbarVisibility();
			element.horizontalScrollbarSpacing = GetHorizontalScrollbarSpacing();
			
			element.vertical = UseVertical();
			element.verticalScrollbarVisibility = GetVerticalScrollbarVisibility();
			element.verticalScrollbarSpacing = GetVerticalScrollbarSpacing();

			element.movementType = GetMovementType();
			element.elasticity = GetElasticity();
			element.inertia = UseIntertia();
			element.decelerationRate = GetDecelerationRate();
			element.scrollSensitivity = GetSensivity();
		}

		public override void Copy(UnityEngine.UI.ScrollRect element)
		{
			horizontal = element.horizontal;
			horizontalScrollbarVisibility = element.horizontalScrollbarVisibility;
			horizontalSpacing = element.horizontalScrollbarSpacing;

			vertical = element.vertical;
			verticalScrollbarVisibility = element.verticalScrollbarVisibility;
			verticalSpacing = element.verticalScrollbarSpacing;

			movementType = element.movementType;
			elasticity = element.elasticity;
			inertia = element.inertia;
			decelerationRate = element.decelerationRate;
			scrollSensivity = element.scrollSensitivity;
		}

		public float GetDecelerationRate()
		{
			return decelerationRate;
		}

		public float GetElasticity()
		{
			return elasticity;
		}

		public float GetHorizontalScrollbarSpacing()
		{
			return horizontalSpacing;
		}

		public UnityEngine.UI.ScrollRect.ScrollbarVisibility GetHorizontalScrollbarVisibility()
		{
			return horizontalScrollbarVisibility;
		}

		public UnityEngine.UI.ScrollRect.MovementType GetMovementType()
		{
			return movementType;
		}

		public float GetSensivity()
		{
			return scrollSensivity;
		}

		public float GetVerticalScrollbarSpacing()
		{
			return verticalSpacing;
		}

		public UnityEngine.UI.ScrollRect.ScrollbarVisibility GetVerticalScrollbarVisibility()
		{
			return verticalScrollbarVisibility;
		}

		public bool UseHorizontal()
		{
			return horizontal;
		}

		public bool UseIntertia()
		{
			return inertia;
		}

		public bool UseVertical()
		{
			return vertical;
		}
	}
}
