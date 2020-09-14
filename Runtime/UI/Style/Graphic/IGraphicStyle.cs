using UnityEngine;

namespace REF.Runtime.UI.Style.Graphic
{
	public interface IGraphicStyle
	{
		bool IsRaycastTarget();
		Color GetColor();
		Material GetMaterial();
	}

	public interface IMaskableGraphicStyle : IGraphicStyle
	{
		bool IsMaskable();
	}
}
