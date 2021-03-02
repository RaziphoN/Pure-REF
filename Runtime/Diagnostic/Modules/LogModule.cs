using UnityEngine;

using System.Collections.Generic;

namespace REF.Runtime.Diagnostic.Modules
{
	[CreateAssetMenu(fileName = "LogModule", menuName = "REF/Diagnostic/Console/Log Module")]
	public class LogModule : DebugModule
	{
		private const int logSize = 1024;

		[System.NonSerialized] private int currentIdx = -1;
		[System.NonSerialized] private int[] logCountByType = new int[(int)LogType.Exception + 1];

		[System.NonSerialized] private bool showInfoLog = true;
		[System.NonSerialized] private bool showWarningLog = true;
		[System.NonSerialized] private bool showErrorLog = true;
		[System.NonSerialized] private bool showFatalLog = true;

		[System.NonSerialized] private Vector2 scrollLogView = Vector2.zero;
		[System.NonSerialized] private Vector2 scrollCurrentLogView = Vector2.zero;

		[System.Serializable]
		public class Record
		{
			public LogType Type;
			public string ShowName;
			public string Time;
			public string Message;
			public string StackTrace;
		}

		[System.Serializable]
		public class SystemInfo
		{
			// public string AppVersion;

			public string Platform;
			public string OperationSystem;

			public string DeviceID;
			public string DeviceName;
			public string DeviceType;
			public string DeviceModel;
			public string DeviceAvailableMemory;

			public string ProcessorType;
			public string ProcessorCoreCount;
			public string ProcessorFrequency;

			public string GraphicsAPIVersion;
			public string GraphicsVendor;
			public string GraphicsTotalMemory;
			public string GraphicsMaxTextureSize;
		}

		[SerializeField] private SystemInfo systemInfo = new SystemInfo();
		[SerializeField] private List<Record> logs = new List<Record>(logSize);

		public override string GetTitle()
		{
			return "Log";
		}

		public override void OnInit()
		{
			base.OnInit();

			systemInfo.Platform = Application.platform.ToString();
			systemInfo.OperationSystem = UnityEngine.SystemInfo.operatingSystem;
			systemInfo.DeviceID = UnityEngine.SystemInfo.deviceUniqueIdentifier;
			systemInfo.DeviceName = UnityEngine.SystemInfo.deviceName;
			systemInfo.DeviceType = UnityEngine.SystemInfo.deviceType.ToString();
			systemInfo.DeviceModel = UnityEngine.SystemInfo.deviceModel;
			systemInfo.DeviceAvailableMemory = UnityEngine.SystemInfo.systemMemorySize.ToString();
			systemInfo.ProcessorType = UnityEngine.SystemInfo.processorType;
			systemInfo.ProcessorCoreCount = UnityEngine.SystemInfo.processorCount.ToString();
			systemInfo.ProcessorFrequency = UnityEngine.SystemInfo.processorFrequency.ToString();
			systemInfo.GraphicsAPIVersion = UnityEngine.SystemInfo.graphicsDeviceVersion;
			systemInfo.GraphicsVendor = UnityEngine.SystemInfo.graphicsDeviceVendor;
			systemInfo.GraphicsTotalMemory = UnityEngine.SystemInfo.graphicsMemorySize.ToString();
			systemInfo.GraphicsMaxTextureSize = UnityEngine.SystemInfo.maxTextureSize.ToString();


			Application.logMessageReceived += OnLogReceivedHandler;
		}

		public override void OnGui(Rect rect)
		{
			base.OnGui(rect);

			GUILayout.BeginHorizontal();
			GUI.contentColor = Color.white;

			if (GUILayout.Button("Save", GUILayout.Width(40)))
			{
				Save();
			}

			if (GUILayout.Button("Clear", GUILayout.Width(40)))
			{
				currentIdx = -1;
				
				for (int idx = 0; idx < logCountByType.Length - 1; ++idx)
				{
					logCountByType[idx] = 0;
				}

				logs.Clear();
			}

			GUI.contentColor = (showInfoLog ? Color.white : Color.gray);
			showInfoLog = GUILayout.Toggle(showInfoLog, $"Info [{logCountByType[(int)LogType.Log]}]");
			GUI.contentColor = (showWarningLog ? Color.white : Color.gray);
			showWarningLog = GUILayout.Toggle(showWarningLog, $"Warning [{logCountByType[(int)LogType.Warning]}]");
			GUI.contentColor = (showErrorLog ? Color.white : Color.gray);
			showErrorLog = GUILayout.Toggle(showErrorLog, $"Error [{logCountByType[(int)LogType.Error]}]");
			GUI.contentColor = (showFatalLog ? Color.white : Color.gray);
			showFatalLog = GUILayout.Toggle(showFatalLog, $"Fatal [{logCountByType[(int)LogType.Exception] + logCountByType[(int)LogType.Assert]}]");
			GUI.contentColor = Color.white;

			GUILayout.EndHorizontal();

			scrollLogView = GUILayout.BeginScrollView(scrollLogView, "Box", GUILayout.Height(165));
			for (int i = 0; i < logs.Count; i++)
			{
				bool show = false;
				Color color = Color.white;
				switch (logs[i].Type)
				{
					case LogType.Assert:
					case LogType.Exception:
					{
						show = showFatalLog;
						color = Color.red;
					}
					break;

					case LogType.Error:
					{
						show = showErrorLog;
						color = Color.red;
					}
					break;

					case LogType.Log:
					{
						show = showInfoLog;
						color = Color.white;
					}
					break;

					case LogType.Warning:
					{
						show = showWarningLog;
						color = Color.yellow;
					}
					break;

					default:
					break;
				}

				if (show)
				{
					GUILayout.BeginHorizontal();
					GUI.contentColor = color;

					if (GUILayout.Toggle(currentIdx == i, logs[i].ShowName))
					{
						currentIdx = i;
					}

					GUILayout.FlexibleSpace();
					GUI.contentColor = Color.white;
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndScrollView();

			scrollCurrentLogView = GUILayout.BeginScrollView(scrollCurrentLogView, "Box", GUILayout.Height(100));

			if (currentIdx != -1)
			{
				GUILayout.Label(logs[currentIdx].Message + "\r\n\r\n" + logs[currentIdx].StackTrace);
			}

			GUILayout.EndScrollView();
		}

		public override void OnRelease()
		{
			Application.logMessageReceived -= OnLogReceivedHandler;
			base.OnRelease();
		}

		private void OnLogReceivedHandler(string condition, string stackTrace, LogType type)
		{
			Record log = new Record();
			log.Time = System.DateTime.Now.ToString("HH:mm:ss");
			log.Message = condition;
			log.StackTrace = stackTrace;

			log.Type = type;
			logCountByType[(int)type]++;

			log.ShowName += $"[{log.Type}][{log.Time}] {log.Message}";

			if (logs.Count >= logSize)
			{
				logs.RemoveAt(0);
			}

			logs.Add(log);
		}


		private void Save()
		{
			var timestamp = System.DateTime.Now.ToString("MM-dd-yyyy_HH-mm");
			var path = $"{Application.persistentDataPath}/log_{timestamp}.json";
			var json = JsonUtility.ToJson(this);

			if (!System.IO.File.Exists(path))
			{
				System.IO.File.Create(path);
			}

			System.IO.File.WriteAllText(path, json);

			RefDebug.Log(nameof(LogModule), "Saved logs!");
		}
	}
}
