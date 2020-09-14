using UnityEngine;

namespace REF.Runtime.UI.Style.Graphic
{
	[System.Serializable]
	public class GraphicStyle : Style<UnityEngine.UI.Graphic>, IGraphicStyle
	{
		[SerializeField] Color color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		[SerializeField] Material material = null;
		[SerializeField] bool raycastTarget = true;

		public override void Apply(UnityEngine.UI.Graphic element)
		{
			element.color = GetColor();
			element.material = GetMaterial();
			element.raycastTarget = IsRaycastTarget();
		}

		public Color GetColor()
		{
			return color;
		}

		public Material GetMaterial()
		{
			return material;
		}

		public bool IsRaycastTarget()
		{
			return raycastTarget;
		}
	}
}
