using UnityEngine;

namespace REF.Runtime.Diagnostic.Modules
{
	// Each module is pretend to be just ui window inside Debug Console
	public abstract class DebugModule : ScriptableObject, IConsoleModule
	{
		public abstract string GetTitle();

		public virtual void OnInit()
		{
			
		}

		public virtual void OnShow()
		{
			
		}

		public virtual void OnGui(Rect rect)
		{

		}

		public virtual void OnUpdate()
		{
			
		}

		public virtual void OnHide()
		{
			
		}

		public virtual void OnRelease()
		{
			
		}
	}
}
