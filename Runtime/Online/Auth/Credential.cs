#if REF_ONLINE_AUTH

using UnityEngine;

namespace REF.Runtime.Online.Auth
{
	public enum ProviderType
	{
		Unknown,
		Anonymous,
		EmailPassword,
		Custom,

		Facebook,
		Apple,
		// TODO: Add here
	}

	[System.Serializable]
	public class Credential
	{
		[SerializeField] private ProviderType provider = ProviderType.Unknown;
		[SerializeField] private string token;
		[SerializeField] private string nonce;
		[SerializeField] private string email;
		[SerializeField] private string phoneNumber;
		[SerializeField] private string password;

		public bool IsValid()
		{
			switch (provider)
			{
				case ProviderType.Unknown:
				{
					return false;
				}
				break;

				case ProviderType.Anonymous:
				{
					return true;
				}
				break;

				case ProviderType.Custom:
				case ProviderType.Facebook:
				{
					return !string.IsNullOrEmpty(token);
				}
				break;

				case ProviderType.Apple:
				{
					return !string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(nonce);
				}
				break;

				case ProviderType.EmailPassword:
				{
					return !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password);
				}
				break;
			}

			Debug.LogError("Unknown credential type, probably isn't implemented");
			return false;
		}

		public void SetUpAnonymous()
		{
			this.provider = ProviderType.Anonymous;
		}

		public void SetUpEmailPassword(string email, string password)
		{
			this.provider = ProviderType.EmailPassword;
			this.email = email;
			this.password = password;
		}

		public void SetUpToken(ProviderType provider, string token, string nonce = null)
		{
			if (provider == ProviderType.Anonymous || provider == ProviderType.EmailPassword)
			{
				return;
			}

			this.provider = provider;
			this.token = token;
			this.nonce = null;
		}

		public ProviderType GetProvider()
		{
			return provider;
		}

		public string GetToken()
		{
			return token;
		}

		public string GetNonce()
		{
			return nonce;
		}

		public string GetEmail()
		{
			return email;
		}

		public string GetPassword()
		{
			return password;
		}

		public string GetPhoneNumber()
		{
			return phoneNumber;
		}
	}
}

#endif