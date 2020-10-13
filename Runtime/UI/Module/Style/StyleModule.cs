using UnityEngine;
using UnityEngine.EventSystems;

using REF.Runtime.UI.Style;

namespace REF.Runtime.UI.Module.Style
{
	public class StyleModule<T, U> : ModuleBase where T : UIBehaviour where U : StyleObject<T>
	{
		[SerializeField] protected U style;

		public override void OnInit()
		{
			base.OnInit();
			Apply();
		}

		public override void OnEditorValidate()
		{
			base.OnEditorValidate();
			
			if (!Application.isPlaying)
			{
				Apply();
			}
		}

		public void Apply()
		{
			var target = GetTarget<T>();

			if (target != null)
			{
				style?.Apply(target);
			}
		}

#if UNITY_EDITOR
		public U CreateStyle()
		{
			var target = GetTarget<T>();

			if (target != null)
			{
				var template = ScriptableObject.CreateInstance<U>();
				template.Copy(target);

				UnityEditor.AssetDatabase.CreateAsset(template, $"Assets/{typeof(U).Name}.asset");
				this.style = template;

				return template;
			}

			return null;
		}
#endif
	}
}
