using System.Collections.Generic;

namespace REF.Runtime.Notifications
{
	public class Notification : INotification
	{
		private string title;
		private string body;
		private Dictionary<string, string> data = new Dictionary<string, string>();

		public string Title { get { return title; } set { title = value; } }
		public string Body { get { return body; } set { body = value; } }

		public Notification(string title, string body, IDictionary<string, string> data = null)
		{
			this.title = title;
			this.body = body;

			this.data.Clear();

			if (data != null)
			{
				foreach (var pair in data)
				{
					this.data.Add(pair.Key, pair.Value);
				}
			}
		}

		public bool ContainsKey(string key)
		{
			return data.ContainsKey(key);
		}

		public void Set(string key, string value)
		{
			if (!data.ContainsKey(key))
			{
				data.Add(key, value);
			}
			else
			{
				data[key] = value;
			}
		}

		public void Remove(string key)
		{
			data.Remove(key);
		}

		public string Get(string key)
		{
			return data[key];
		}

		public IEnumerable<string> GetKeys()
		{
			return data.Keys;
		}
	}
}
