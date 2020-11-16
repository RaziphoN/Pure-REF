#if REF_ONLINE_AUTH
using UnityEngine;

using System.Collections.Generic;

namespace REF.Runtime.Online.Auth
{
	[System.Serializable]
	public class User
	{
		[SerializeField] private string providerId;
		[SerializeField] private string displayName;
		[SerializeField] private string id;
		[SerializeField] private string token;
		[SerializeField] private string email;
		[SerializeField] private string phoneNumber;
		private System.Uri photoUri;

		private IDictionary<string, string> data = new Dictionary<string, string>();

		public void SetData(IDictionary<string, string> data)
		{
			if (data != null)
			{
				this.data = data;
			}
			else
			{
				this.data.Clear();
			}
		}

		public void SetKey(string key, string value)
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

		public void SetProvider(string providerId)
		{
			this.providerId = providerId;
		}

		public void SetDisplayName(string name)
		{
			displayName = name;
		}

		public void SetPhotoUri(System.Uri uri)
		{
			photoUri = uri;
		}

		public void SetPhoneNumber(string phoneNumber)
		{
			this.phoneNumber = phoneNumber;
		}

		public void SetToken(string token)
		{
			this.token = token;
		}

		public void SetUid(string id)
		{
			this.id = id;
		}

		public void SetEmail(string email)
		{
			this.email = email;
		}

		public string GetProvider()
		{
			return providerId;
		}

		public string GetDisplayName()
		{
			return displayName;
		}

		public string GetUid()
		{
			return id;
		}

		public string GetToken()
		{
			return token;
		}

		public string GetEmail()
		{
			return email;
		}

		public string GetPhoneNumber()
		{
			return phoneNumber;
		}

		public System.Uri GetPhotoUri()
		{
			return photoUri;
		}

		public bool HasKey(string key)
		{
			return data.ContainsKey(key);
		}

		public string GetKey(string key)
		{
			return data[key];
		}

		public IDictionary<string, string> GetData()
		{
			return data;
		}
	}
}

#endif