using System.Collections.Generic;

namespace REF.Runtime.Notifications
{
	public interface INotification
	{
		string Title { get; set; }
		string Body { get; set; }
		IDictionary<string, string> Data { get; set; }
	}
}
