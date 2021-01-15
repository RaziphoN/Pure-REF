#if REF_ONLINE_ADVERTISMENT

namespace REF.Runtime.Online.Advertisments
{
	// NOTE: All callbacks should be assumed as async (outside main thread)
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
