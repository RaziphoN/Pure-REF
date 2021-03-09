using UnityEngine;

using REF.Runtime.Diagnostic.Modules;

namespace REF.Runtime.Diagnostic
{
	[System.Serializable]
	public class ConsoleConfiguration
	{
		[SerializeField] private Rect minimizedWindowRect = new Rect(0, 0, 140, 60);
		[SerializeField] private Vector2Int nativeSize = new Vector2Int(800, 600);
		[SerializeField] private DebugModule[] modules;

		public IConsoleModule[] GetModules()
		{
			return modules;
		}

		public Rect GetMinimizedWindowRect()
		{
			return minimizedWindowRect;
		}

		public Vector2Int GetNativeSize()
		{
			return nativeSize;
		}
	}
}
