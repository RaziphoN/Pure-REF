using UnityEngine;

using System.Collections.Generic;

using REF.Runtime.Utilities.Serializable;

namespace REF.Runtime.Notifications
{
	[CreateAssetMenu(fileName = "Scriptable Notification", menuName = "REF/Notifications/Local Notification")]
	public class ScriptableLocalNotification : ScriptableObject, ILocalNotification
	{
		[SerializeField] private string title = string.Empty;
		[SerializeField] private string body = string.Empty;
		[SerializeField] private ScriptableNotificationSettings settings;
		[SerializeField] private SerializableStringDictionary data = new SerializableStringDictionary();
		private IDictionary<string, string> setData = null;

		public string Title { get { return title; } set { title = value; } }
		public string Body { get { return body; } set { body = value; } }
		public INotificationSettings Settings { get { return settings.Settings; } set { settings.Settings = value; } }

		public IDictionary<string, string> Data
		{
			get
			{
				if (setData != null)
					return setData;
				
				return data;
			}

			set
			{
				setData = value;
				data = null;
			}
		}
	}
}
