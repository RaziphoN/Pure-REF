using UnityEngine;
using UnityEngine.EventSystems;

namespace REF.Runtime.UI.Module
{
	public interface IModularBehaviour
	{
		ModuleHandler<T> GetHandler<T>() where T : ModuleBase;
	}

	public interface IModule
	{
		void SetTarget(UIBehaviour target);
		T GetTarget<T>() where T : UIBehaviour;

		void OnInit();
		void OnShow();

		void OnHide();
		void OnDeinit();

		void OnEditorValidate();
	}

	[System.Serializable]
	public class ModuleHandler : ModuleHandler<ModuleBase> { }

	[System.Serializable]
	public abstract class ModuleBase : MonoBehaviour, IModule
	{
		private UIBehaviour target;

		public void SetTarget(UIBehaviour target)
		{
			this.target = target;
		}

		public T GetTarget<T>() where T : UIBehaviour { return target as T; }

		public virtual void OnInit() { }
		public virtual void OnShow() { }
		public virtual void OnHide() { }
		public virtual void OnDeinit() { }

		public virtual void OnEditorValidate() { }

#if UNITY_EDITOR
		private void Reset()
		{
			var behaviour = GetComponent<IModularBehaviour>();
			
			if (behaviour != null)
			{
				var handler = behaviour.GetHandler<ModuleBase>();
				handler.AddModule(this);
			}
		}

		// TODO: Fix
		private void OnDestroy()
		{
			var behaviour = GetComponent<IModularBehaviour>();

			if (behaviour != null)
			{
				var handler = behaviour.GetHandler<ModuleBase>();
				handler.RemoveModule(this);
			}
		}
#endif
	}
}
