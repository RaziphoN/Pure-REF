#if REF_ONLINE_ADVERTISMENT

namespace REF.Runtime.Online.Advertisments
{
	public enum BannerPosition
	{
		TopLeft,
		TopCenter,
		TopRight,
		MiddlefLeft,
		Center,
		MiddleRight,
		BottomLeft,
		BottomCenter,
		BottomRight
	}

	public interface IBannerAd : System.IDisposable
	{
		event System.Action OnLoaded;
		event System.Action OnFailedToLoad;
		event System.Action OnShow;
		event System.Action OnHide;
		event System.Action OnLeaveApplication;

		AdState GetState();
		string GetPlacement(); // in some cases it may be called as ad unit id

		void Load();
		void Show();
		void Hide();
	}
}

#endif
