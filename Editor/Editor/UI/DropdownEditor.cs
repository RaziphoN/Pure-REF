﻿using UnityEditor;

using REF.Runtime.UI;

namespace REF.Editor.UI
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(Dropdown))]
	public class DropdownEditor : UnityEditor.UI.DropdownEditor
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
