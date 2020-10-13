
namespace REF.Runtime.UI.Module
{
	public interface IButtonModule : IModule
	{
		void OnClick();
	}

	[System.Serializable]
	public class ButtonModuleHandler : ModuleHandler<ButtonModuleBase> { }

	public class ButtonModuleBase : ModuleBase
	{
		public virtual void OnClick()
		{

		}
	}
}
