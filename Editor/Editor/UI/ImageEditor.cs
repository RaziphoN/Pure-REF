using UnityEditor;

using REF.Runtime.UI;

namespace REF.Editor.Editor.UI
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Image))]
	public class ImageEditor : UnityEditor.UI.ImageEditor
	{
		private SerializedProperty styleProperty;

		protected override void OnEnable()
		{
			base.OnEnable();
			styleProperty = serializedObject.FindProperty("style");
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			EditorGUILayout.PropertyField(styleProperty);
			serializedObject.ApplyModifiedProperties();
		}
	}
}
