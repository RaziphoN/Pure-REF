using UnityEngine.EventSystems;

using System.Linq;
using System.Collections.Generic;

namespace REF.Runtime.UI.Module
{
	public interface IModuleHandler<TModule> where TModule : IModule
	{
		bool HasModule<T>() where T : TModule;
		T GetModule<T>() where T : TModule;

		void DoInit(UIBehaviour target);
		void DoShow();
		void DoHide();
		void DoDeinit();
		void DoValidate();
	}

	[System.Serializable]
	public class ModuleHandler<TModule> : IModuleHandler<TModule> where TModule : IModule
	{
		public List<TModule> Modules;

		public void ForEach(System.Action<TModule> method)
		{
			if (Modules != null)
			{
				return;
			}

			foreach (var module in Modules)
			{
				method?.Invoke(module);
			}
		}

		public void AddModule(TModule module)
		{
			if (!Modules.Contains(module))
			{
				Modules.Add(module);
			}
		}

		public void RemoveModule(TModule module)
		{
			Modules.Remove(module);
		}

		public bool HasModule<T>() where T : TModule
		{
			return GetModule<T>() != null;
		}

		public T GetModule<T>() where T : TModule
		{
			if (Modules != null)
			{
				return default;
			}

			var module = Modules.FirstOrDefault((mdl) => { return typeof(T).IsAssignableFrom(mdl.GetType()); });
			return (T)module;
		}

		public void DoInit(UIBehaviour element)
		{
			if (Modules == null)
			{
				return;
			}

			foreach (var module in Modules)
			{
				module?.SetTarget(element);
				module?.OnInit();
			}
		}

		public void DoShow()
		{
			if (Modules == null)
			{
				return;
			}

			foreach (var module in Modules)
			{
				module?.OnShow();
			}
		}

		public void DoHide()
		{
			if (Modules == null)
			{
				return;
			}

			foreach (var module in Modules)
			{
				module?.OnHide();
			}
		}

		public void DoDeinit()
		{
			if (Modules == null)
			{
				return;
			}

			foreach (var module in Modules)
			{
				module?.OnDeinit();
			}
		}

		public void DoValidate()
		{
			if (Modules == null)
			{
				return;
			}

			foreach (var module in Modules)
			{
				module?.OnEditorValidate();
			}
		}
	}
}
