#if REF_ONLINE_AUTH
using UnityEngine;

using System.Collections.Generic;

namespace REF.Runtime.Online.Auth
{
	[System.Serializable]
	public class ProviderData
	{
		[SerializeField] private string displayName;
		[SerializeField] private string email;
		[SerializeField] private string photoUrl;
		[SerializeField] private string providerId;
		[SerializeField] private string userId;

		public ProviderData(string providerId, string displayName, string email, System.Uri photoUrl, string userId)
		{
			this.providerId = providerId;
			this.userId = userId;

			this.email = email;

			if (photoUrl != null)
			{
				this.photoUrl = photoUrl.ToString();
			}

			this.displayName = displayName;
		}

		public void Copy(ProviderData other)
		{
			providerId = other.providerId;
			userId = other.userId;
			email = other.email;
			photoUrl = other.photoUrl;
			displayName = other.displayName;
		}

		public string GetProviderId()
		{
			return providerId;
		}

		public string GetUserId()
		{
			return userId;
		}

		public System.Uri GetPhotoUrl()
		{
			return new System.Uri(photoUrl);
		}

		public string GetDisplayName()
		{
			return displayName;
		}

		public string GetEmail()
		{
			return email;
		}
	}

	[System.Serializable]
	public class User
	{
		[SerializeField] private string providerId;
		[SerializeField] private string displayName;
		[SerializeField] private string id;
		[SerializeField] private string token;
		[SerializeField] private string email;
		[SerializeField] private string phoneNumber;
		[SerializeField] private List<ProviderData> providerData = new List<ProviderData>();

		private System.Uri photoUrl;

		private IDictionary<string, string> data = new Dictionary<string, string>();

		public void AddProviderData(ProviderData data)
		{
			var providerId = data.GetProviderId();
			var foundData = GetProviderData(providerId);

			if (foundData == null)
			{
				providerData.Add(data);
			}
			else
			{
				foundData.Copy(data);
			}
		}

		public ProviderData GetProviderData(string providerId)
		{
			var found = providerData.Find((data) => { return data.GetProviderId() == providerId; });
			return found;
		}

		public void RemoveProviderData(string providerId)
		{
			var found = GetProviderData(providerId);
			if (found != null)
			{
				providerData.Remove(found);
			}
		}

		public void ClearProviderData()
		{
			providerData.Clear();
		}

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

		public void SetPhotoUrl(System.Uri url)
		{
			photoUrl = url;
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

		public System.Uri GetPhotoUrl()
		{
			return photoUrl;
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