using UnityEngine;
using UnityEngine.EventSystems;

using REF.Runtime.UI.Module;

namespace REF.Runtime.UI
{
	[AddComponentMenu("UI/REF/Slider")]
	public class Slider : UnityEngine.UI.Slider, IModularBehaviour
	{
		public event System.Action OnUserDragStart;
		public event System.Action<float> OnUserDragEnd;

		[SerializeField] private ModuleHandler handler = new ModuleHandler();

		public ModuleHandler<T> GetHandler<T>() where T : ModuleBase
		{
			return handler as ModuleHandler<T>;
		}

		public override void OnPointerDown(PointerEventData eventData)
		{
			base.OnPointerDown(eventData);
			OnUserDragStart?.Invoke();
		}

		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			OnUserDragEnd?.Invoke(value);
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
