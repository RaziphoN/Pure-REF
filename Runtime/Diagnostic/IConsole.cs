namespace REF.Runtime.Diagnostic
{
	public interface IConsole
	{
		void OnInit();

		void OnShow();
		void OnUpdate();
		void OnGui();
		void OnHide();

		void OnRelease();
	}
}
