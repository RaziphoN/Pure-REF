using UnityEngine;

using REF.Runtime.UI.Module;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Scroll Rect")]
	[SelectionBase]
	public class ScrollRect : UnityEngine.UI.ScrollRect, IModularBehaviour
	{
		[SerializeField] private ModuleHandler handler = new ModuleHandler();

		public ModuleHandler<T> GetHandler<T>() where T : ModuleBase
		{
			return handler as ModuleHandler<T>;
		}

		protected override void Awake()
		{
			base.Awake();
			handler.DoInit(this);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			handler.DoShow();
		}

		protected override void OnDisable()
		{
			handler.DoHide();
			base.OnDisable();
		}

		protected override void OnDestroy()
		{
			handler.DoDeinit();
			base.OnDestroy();
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			handler.DoInit(this);
			handler.DoValidate();
		}
	}
}
