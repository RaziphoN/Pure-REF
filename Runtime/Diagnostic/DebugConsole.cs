using UnityEngine;

using REF.Runtime.Diagnostic.Modules;

namespace REF.Runtime.Diagnostic
{
	// TODO: This object will be created by Diagnostic tool for RefDebug if required
	[System.Serializable]
	public class DebugConsole : MonoBehaviour, IConsole
	{
		[SerializeField] private Rect windowRect = new Rect(0, 0, 100, 60);
		[SerializeField] private Vector2 nativeSize = new Vector2(1920, 1080);

		private Vector2 lastDragPos;
		private bool expansion = false;
		private int fps = 0;
		private float lastShowFPSTime = 0f;
		private bool expansionFlag = false;

		// one module is active at a time
		[SerializeField] private DebugModule[] modules;
		private DebugModule current;

		public void OnInit()
		{
#if REF_DEBUG_CONSOLE
			for (int idx = 0; idx < modules.Length; ++idx)
			{
				var module = modules[idx];
				module.OnInit();
			}
#endif
		}

		public void OnShow()
		{
#if REF_DEBUG_CONSOLE

#endif
		}

		public void OnUpdate()
		{
#if REF_DEBUG_CONSOLE
			FPSUpdate();
#endif
		}

		public void OnGui()
		{
#if REF_DEBUG_CONSOLE
			Vector3 scale = new Vector3(Screen.width / nativeSize.x, Screen.height / nativeSize.y, 1.0f);
			GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, scale);

			if (expansion)
			{
				windowRect = GUI.Window(0, windowRect, DrawFull, "Debug");
			}
			else
			{
				windowRect = GUI.Window(0, windowRect, DrawMinimized, "Debug");
			}
#endif
		}

		public void OnHide()
		{
#if REF_DEBUG_CONSOLE

#endif
		}

		public void OnRelease()
		{
#if REF_DEBUG_CONSOLE
			for (int idx = modules.Length - 1; idx >= 0; --idx)
			{
				var module = modules[idx];
				module.OnRelease();
			}
#endif
		}

		private void Start()
		{
#if REF_DEBUG_CONSOLE
			OnInit();
			OnShow();
#endif
		}

		private void Update()
		{
#if REF_DEBUG_CONSOLE
			OnUpdate();
#endif
		}

		private void OnDestroy()
		{
#if REF_DEBUG_CONSOLE
			OnHide();
			OnRelease();
#endif
		}

		private void OnGUI()
		{
#if REF_DEBUG_CONSOLE
			OnGui();
#endif
		}

        private void DrawButtonForModule(int idx)
		{
            var module = modules[idx];
            var title = module.GetTitle();

            if (GUILayout.Button(title, GUILayout.Height(30)))
            {
                current?.OnHide();
                current = module;
                current?.OnShow();
            }
        }

        private void HeaderGUI()
        {
            if (nativeSize.x < nativeSize.y)
            {
                GUILayout.BeginVertical();
            }

            GUILayout.BeginHorizontal();

            GUI.contentColor = Color.white;

            if (GUILayout.Button("Minimize", GUILayout.Height(30)))
            {
                expansion = false;
				expansionFlag = false;

				windowRect.x = lastDragPos.x;
				windowRect.y = lastDragPos.y;
				windowRect.width = 100;
                windowRect.height = 60;
            }

			GUILayout.Label($"FPS: {fps}");

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            for (int idx = 0; idx < modules.Length; ++idx)
			{
                if (idx % 4 == 0)
				{
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                }

                DrawButtonForModule(idx);
			}
            GUILayout.EndHorizontal();

            if (nativeSize.x < nativeSize.y)
            {
                GUILayout.EndVertical();
            }
        }

		private void FPSUpdate()
		{
			float time = Time.realtimeSinceStartup - lastShowFPSTime;
			if (time >= 1)
			{
				fps = (int)(1.0f / Time.deltaTime);
				lastShowFPSTime = Time.realtimeSinceStartup;
			}
		}

		private void DrawFull(int windowId)
		{
			HeaderGUI();
			current?.OnGui(windowRect);
		}

		private void DrawMinimized(int windowId)
		{
			GUI.contentColor = Color.white;
			if (GUILayout.Button("Maximize: " + fps, GUILayout.Width(80), GUILayout.Height(30)))
			{
				expansion = true;
				expansionFlag = false;

				lastDragPos.x = windowRect.x;
				lastDragPos.y = windowRect.y;

				windowRect.x = 0;
				windowRect.y = 0;
				windowRect.width = nativeSize.x;
				windowRect.height = (nativeSize.y < 430) ? nativeSize.y : 430;
			}
			GUI.contentColor = Color.white;

			//Rect rect = windowRect;
			//rect.width = 10000;
			//rect.height = 20;
			GUI.DragWindow();
		}
	}
}
