#if REF_ONLINE_AUTH
using System.Linq;
using System.Collections.Generic;

namespace REF.Runtime.Online.Auth
{
	public class Credential
	{
		private string providerId;
		private Dictionary<string, string> data = new Dictionary<string, string>();

		public string GetProviderId()
		{
			return providerId;
		}

		public void SetProviderId(string providerId)
		{
			this.providerId = providerId;
		}

		public bool HasKey(string key)
		{
			return data.ContainsKey(key);
		}

		public void SetData(string key, string data)
		{
			this.data.Add(key, data);
		}

		public string GetData(string key)
		{
			return data[key];
		}

		public List<string> GetKeys()
		{
			return data.Keys.ToList();
		}

		public void SetToken(string token)
		{
			SetData("token", token);
		}

		public string GetToken()
		{
			return data["token"];
		}

		public void SetRawNonce(string nonce)
		{
			SetData("rawNonce", nonce);
		}

		public void SetNonce(string nonce)
		{
			SetData("nonce", nonce);
		}

		public string GetRawNonce()
		{
			return GetData("rawNonce");
		}

		public string GetNonce()
		{
			return GetData("nonce");
		}

		public void SetEmail(string email)
		{
			SetData("email", email);
		}

		public string GetEmail()
		{
			return GetData("email");
		}

		public void SetPassword(string password)
		{
			SetData("password", password);
		}

		public string GetPassword()
		{
			return GetData("password");
		}

		public bool Equals(Credential other)
		{
			return other != null && providerId == other.providerId
				&& data.Count == other.data.Count && !data.Except(other.data).Any();
		}
	}
}

#endif