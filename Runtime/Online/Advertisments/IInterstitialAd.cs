#if REF_ONLINE_ADVERTISMENT

namespace REF.Runtime.Online.Advertisments
{
	public interface IInterstitialAd : System.IDisposable
	{
		event System.Action OnLoaded;
		event System.Action OnFailedToLoad;
		event System.Action OnShow;
		event System.Action OnHide;
		event System.Action OnLeaveApplication;

		AdState GetState();
		string GetPlacement();

		void Load();
		void Show();
		void Hide();
	}
}

#endif