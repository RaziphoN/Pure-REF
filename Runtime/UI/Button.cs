using UnityEngine;
using UnityEngine.EventSystems;

using REF.Runtime.UI.Module;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Button")]
	public class Button : UnityEngine.UI.Button, IModularBehaviour
	{
		[SerializeField] private ModuleHandler handler = new ModuleHandler();

		public ModuleHandler<T> GetHandler<T>() where T : ModuleBase
		{
			return handler as ModuleHandler<T>;
		}

		public override void OnPointerClick(PointerEventData eventData)
		{
			base.OnPointerClick(eventData);
			handler.ForEach((module) =>
			{
				var btnModule = module as IButtonModule;
				btnModule?.OnClick();
			});
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
