namespace REF.Runtime.Online.DynamicLinks
{
	public interface ILinkProcessor
	{
		int Priority { get; } // the less value = the less priority, higher value = higher priority
		string Pattern { get; } // regular expression to identify thetype of link you want to process with your processor
		
		void Handle(System.Uri link);
	}
}
