using UnityEngine;

using System.Collections.Generic;

namespace REF.Runtime.Notifications
{
	[CreateAssetMenu(fileName = "LocalNotification", menuName = "REF/Notifications/Local Notification")]
	public class ScriptableLocalNotification : ScriptableObject, ILocalNotification
	{
		[System.Serializable]
		public class Pair
		{
			public string Key;
			public string Value;

			public Pair(string key, string value)
			{
				this.Key = key;
				this.Value = value;
			}
		}

		[SerializeField] private string title;
		[SerializeField] private string body;
		[SerializeField] private List<Pair> data = new List<Pair>();

		[SerializeField] private ScriptableNotificationSettings settings;

		public INotificationSettings Settings { get { return settings.ToSettings(); } }
		public string Title { get { return title; } set { title = value; } }
		public string Body { get { return body; } set { body = value; } }

		public bool ContainsKey(string key)
		{
			var found = GetInternal(key);
			return found != null;
		}

		public void Set(string key, string value)
		{
			var found = GetInternal(key);
			
			if (found != null)
			{
				found.Value = value;
			}
			else
			{
				data.Add(new Pair(key, value));
			}
		}

		public void Remove(string key)
		{
			var found = GetInternal(key);
			
			if (found != null)
			{
				data.Remove(found);
			}
		}

		public string Get(string key)
		{
			var found = GetInternal(key);
			return found?.Value;
		}

		public IEnumerable<string> GetKeys()
		{
			return data.ConvertAll((pair) => { return pair.Key; });
		}

		private Pair GetInternal(string key)
		{
			var found = data.Find((pair) => { return pair.Key == key; });
			return found;
		}
	}
}
