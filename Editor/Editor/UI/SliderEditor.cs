using UnityEditor;

using REF.Runtime.UI;

namespace REF.Editor.UI
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Slider))]
	public class SliderEditor : UnityEditor.UI.SliderEditor
	{
		private SerializedProperty handlerProperty;

		protected override void OnEnable()
		{
			base.OnEnable();
			handlerProperty = serializedObject.FindProperty("handler");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.PropertyField(handlerProperty);
			serializedObject.ApplyModifiedProperties();
		}
	}
}
