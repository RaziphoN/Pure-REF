using UnityEditor;

using REF.Runtime.UI;

namespace REF.Editor.UI
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Image))]
	public class ImageEditor : UnityEditor.UI.ImageEditor
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
