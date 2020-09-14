using UnityEngine;
using UnityEditor;

using System.IO;
using System.Text;

namespace REF.Editor
{
	public static class FrameworkUtility
	{
		internal enum ScriptType
		{
			Unknown,
			Event,
			EventListener,
			Variable,
		}

		internal static ScriptType TagToType(string tag)
		{
			switch (tag)
			{
				case "VARIABLE":
				{
					return ScriptType.Variable;
				}

				case "EVENT":
				{
					return ScriptType.Event;
				}

				case "EVENTLISTENER":
				{
					return ScriptType.EventListener;
				}

				default:
				{
					return ScriptType.Unknown;
				}
			}
		}

		/// <summary>Creates a new C# Class.</summary>
		[MenuItem("Assets/Create/Framework/Event Script", false, 89)]
		private static void CreateEvent()
		{
			string[] guids = AssetDatabase.FindAssets("Framework_TypeEventScript.cs");
			if (guids.Length == 0)
			{
				Debug.LogWarning("Framework_TypeEventScript.cs.txt not found in asset database");
				return;
			}
			string path = AssetDatabase.GUIDToAssetPath(guids[0]);
			CreateFromTemplate(
				"TypeEvent.cs",
				path
			);
		}

		/// <summary>Creates a new C# Class.</summary>
		[MenuItem("Assets/Create/Framework/Event Listener Script", false, 89)]
		private static void CreateEventListener()
		{
			string[] guids = AssetDatabase.FindAssets("Framework_TypeEventListenerScript.cs");
			if (guids.Length == 0)
			{
				Debug.LogWarning("Framework_TypeEventListenerScript.cs.txt not found in asset database");
				return;
			}

			string path = AssetDatabase.GUIDToAssetPath(guids[0]);
			CreateFromTemplate(
				"TypeEventListener.cs",
				path
			);
		}

		[MenuItem("Assets/Create/Framework/Variable Script", false, 89)]
		private static void CreateScriptableVariable()
		{
			string[] guids = AssetDatabase.FindAssets("Framework_TypeVariable.cs");
			if (guids.Length == 0)
			{
				Debug.LogWarning("Framework_TypeVariable.cs.txt not found in asset database");
				return;
			}

			string path = AssetDatabase.GUIDToAssetPath(guids[0]);
			CreateFromTemplate(
				"TypeVariable.cs",
				path
			);
		}

		public static void CreateFromTemplate(string initialName, string templatePath)
		{
			ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
				0,
				ScriptableObject.CreateInstance<DoCreateCodeFile>(),
				initialName,
				EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
				templatePath
			);
		}

		/// Inherits from EndNameAction, must override EndNameAction.Action
		public class DoCreateCodeFile : UnityEditor.ProjectWindowCallback.EndNameEditAction
		{
			public override void Action(int instanceId, string pathName, string resourceFile)
			{
				UnityEngine.Object o = CreateScript(pathName, resourceFile);
				ProjectWindowUtil.ShowCreatedAsset(o);
			}
		}

		/// <summary>Creates Script from Template's path.</summary>
		internal static UnityEngine.Object CreateScript(string pathName, string templatePath)
		{
			string className = Path.GetFileNameWithoutExtension(pathName).Replace(" ", string.Empty);
			string templateText = string.Empty;

			UTF8Encoding encoding = new UTF8Encoding(true, false);

			if (File.Exists(templatePath))
			{

				/// Read procedures.
				StreamReader reader = new StreamReader(templatePath);
				templateText = reader.ReadToEnd();
				reader.Close();

				ScriptType type = TagToType(ParseTag(templateText));
				switch (type)
				{
					case ScriptType.Unknown:
					break;

					case ScriptType.Event:
					{
						className = className.Replace("Event", "");
					}
					break;

					case ScriptType.EventListener:
					{
						className = className.Replace("EventListener", "");
					}
					break;

					case ScriptType.Variable:
					{
						className = className.Replace("Variable", "");
					}
					break;
				}

				templateText = RemoveTag(templateText);
				templateText = templateText.Replace("#SCRIPTNAME#", className);
				templateText = templateText.Replace("#NOTRIM#", string.Empty);
				templateText = templateText.Replace("#NAMESPACE#", GetNamespaceForPath(pathName));
				templateText = templateText.Replace("#EVENT#", string.Empty);
				templateText = templateText.Replace("#EVENTLISTENER#", string.Empty);

				/// You can replace as many tags you make on your templates, just repeat Replace function
				/// e.g.:
				/// templateText = templateText.Replace("#NEWTAG#", "MyText");

				/// Write procedures.

				StreamWriter writer = new StreamWriter(Path.GetFullPath(pathName), false, encoding);
				writer.Write(templateText);
				writer.Close();

				AssetDatabase.ImportAsset(pathName);
				return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
			}
			else
			{
				Debug.LogError(string.Format("The template file was not found: {0}", templatePath));
				return null;
			}
		}

		private static string GetNamespaceForPath(string path)
		{
			path = Path.GetDirectoryName(path);
			path = path.Replace(Path.DirectorySeparatorChar, '.');
			path = path.Replace(Path.PathSeparator, '.');
			path = path.Replace(Path.AltDirectorySeparatorChar, '.');
			path = path.Replace("Assets.", "");

			return path;
		}

		private static string ParseTag(string content)
		{
			var begin = content.IndexOf("#*");
			var end = content.IndexOf("*#");

			if (begin != -1 && end != -1)
			{
				return content.Substring(begin + 2, end - begin - 2);
			}

			return string.Empty;
		}

		private static string RemoveTag(string content)
		{
			var begin = content.IndexOf("#*");
			var end = content.IndexOf("*#");

			if (begin != -1 && end != -1)
			{
				return content.Remove(begin, end - begin + 2);
			}

			return content;
		}
	}
}
