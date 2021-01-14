#if REF_ONLINE_ADVERTISMENT

using UnityEngine;

namespace REF.Runtime.Online.Advertisments
{
	[System.Serializable]
	public class Placement
	{
		[SerializeField] private string android;
		[SerializeField] private string iOS;
		[SerializeField] private string fallback;

		public Placement(string android, string iOS, string fallback)
		{
			this.android = android;
			this.iOS = iOS;
			this.fallback = fallback;
		}

		public string GetPlacement()
		{
#if UNITY_ANDROID
			return android;
#elif UNITY_IOS
			return iOS;
#else
			return fallback;
#endif
		}
	}
}

#endif
