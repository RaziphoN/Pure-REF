using UnityEditor;

using REF.Runtime.UI;

namespace REF.Editor.UI
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(ScrollRect))]
	public class ScrollRectEditor : UnityEditor.UI.ScrollRectEditor
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
