#if REF_ONLINE_AUTH
using UnityEngine;

using System.Linq;
using System.Collections.Generic;

using REF.Runtime.Preference;
using REF.Runtime.Serialization;

namespace REF.Runtime.Online.Auth
{
	[System.Serializable]
	public class Credential : ISerializable
	{
		[SerializeField] private string providerId;
		private Dictionary<string, string> data = new Dictionary<string, string>();

		public byte[] Serialize(ISerializer serializer)
		{
			Serializable serializable = new Serializable();
			serializable.ProviderId = providerId;

			foreach (var record in data)
			{
				var serializableRecord = new Record();
				serializableRecord.Key = record.Key;
				serializableRecord.Value = record.Value;

				serializable.Data.Add(serializableRecord);
			}

			return serializer.Serialize(serializable);
		}

		public void Deserialize(ISerializer serializer, byte[] data)
		{
			var serializable = serializer.Deserialize<Serializable>(data);
			
			this.providerId = serializable.ProviderId;
			this.data.Clear();

			foreach (var record in serializable.Data)
			{
				this.data.Add(record.Key, record.Value);
			}

		}

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

		public void SetToken(string token)
		{
			SetData("token", token);
		}

		public string GetToken()
		{
			return data["token"];
		}

		public void SetNonce(string nonce)
		{
			SetData("nonce", nonce);
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
			return providerId == other.providerId
				&& data.Count == other.data.Count && !data.Except(other.data).Any();
		}

		[System.Serializable]
		private class Record
		{
			public string Key;
			public string Value;
		}

		[System.Serializable]
		private class Serializable
		{
			public List<Record> Data = new List<Record>();
			public string ProviderId;
		}
	}
}

#endif