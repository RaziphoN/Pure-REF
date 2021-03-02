using System.Collections.Generic;

namespace REF.Runtime.Notifications
{
	public interface INotification
	{
		string Title { get; }
		string Body { get; }

		bool ContainsKey(string key);
		
		void Set(string key, string value);
		void Remove(string key);
		
		string Get(string key);
		IEnumerable<string> GetKeys();
	}
}
