﻿#if REF_ONLINE_AUTH

using System;

namespace REF.Runtime.Online.Auth.Method
{
	public class MockupEmailPasswordSignInMethod : ISignInMethod
	{
		public const string ProviderId = "password";

		private string email;
		private string password;

		public MockupEmailPasswordSignInMethod(string email, string password)
		{
			this.email = email;
			this.password = password;
		}

		public string GetProviderId()
		{
			return ProviderId;
		}

		public void SignIn(Action<Credential> OnSuccess, Action OnFail)
		{
			var credential = new Credential();
			credential.SetProviderId(ProviderId);
			credential.SetEmail(email);
			credential.SetPassword(password);

			OnSuccess?.Invoke(credential);
		}

		public void SignOut()
		{
			
		}
	}
}

#endif
