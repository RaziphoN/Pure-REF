using UnityEditor;
using UnityEngine;

using System;
using System.Collections.Generic;

namespace REF.Editor
{
	public static class DrawerUtility
	{
		private static readonly Dictionary<Type, Func<Rect, object, object>> _Fields = new Dictionary<Type, Func<Rect, object, object>>()
		{
			{ typeof(int), (rect, value) => EditorGUI.IntField(rect, (int)value) },
			{ typeof(float), (rect, value) => EditorGUI.FloatField(rect, (float)value) },
			{ typeof(string), (rect, value) => EditorGUI.TextField(rect, (string)value) },
			{ typeof(bool), (rect, value) => EditorGUI.Toggle(rect, (bool)value) },
			{ typeof(Vector2), (rect, value) => EditorGUI.Vector2Field(rect, GUIContent.none, (Vector2)value) },
			{ typeof(Vector3), (rect, value) => EditorGUI.Vector3Field(rect, GUIContent.none, (Vector3)value) },
			{ typeof(Bounds), (rect, value) => EditorGUI.BoundsField(rect, (Bounds)value) },
			{ typeof(Rect), (rect, value) => EditorGUI.RectField(rect, (Rect)value) },
		};

		public static T DoField<T>(Rect rect, Type type, T value)
		{
			Func<Rect, object, object> field;
			if (_Fields.TryGetValue(type, out field))
				return (T)field(rect, value);

			if (type.IsEnum)
				return (T)(object)EditorGUI.EnumPopup(rect, (Enum)(object)value);

			if (typeof(UnityEngine.Object).IsAssignableFrom(type))
				return (T)(object)EditorGUI.ObjectField(rect, (UnityEngine.Object)(object)value, type, true);

			Debug.Log("Type is not supported: " + type);
			return value;
		}
	}
}
