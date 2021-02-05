using UnityEngine;

namespace REF.Runtime.Diagnostic
{
	public interface IConsoleModule
	{
		string GetTitle();

		void OnInit();
		void OnRelease();

		void OnShow();
		void OnGui(Rect rect);
		void OnUpdate();
		void OnHide();
	}
}
