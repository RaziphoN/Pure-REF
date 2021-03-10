#if REF_ONLINE_AUTH

using UnityEngine;
using UnityEngine.SignInWithApple;

using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

using REF.Runtime.Diagnostic;

namespace REF.Runtime.Online.Auth.Method
{
	public class AppleSignInMethod : ISignInMethod
	{
		public const string ProviderId = "apple.com";

		private System.Action<Credential> sucessCallback = null;
		private System.Action failCallback = null;

		private string rawNonce;
		private string nonce;

		public AppleSignInMethod()
		{
			rawNonce = GenerateRandomString(32);
			nonce = GenerateSHA256NonceFromRawNonce(rawNonce);
		}

		public AppleSignInMethod(string rawNonce, string nonce)
		{
			this.rawNonce = rawNonce;
			this.nonce = nonce;
		}

		public string GetProviderId()
		{
			return ProviderId;
		}

		public void SignIn(System.Action<Credential> OnSuccess, System.Action OnFail)
		{
			var signInService = Object.FindObjectOfType<UnityEngine.SignInWithApple.SignInWithApple>();
			
			if (signInService == null)
			{
				OnFail?.Invoke();
				return;
			}

			sucessCallback = OnSuccess;
			failCallback = OnFail;

			signInService.Login(OnSignInRequestHandler);
		}

		public void SignOut()
		{
			
		}

		private void OnSignInRequestHandler(SignInWithApple.CallbackArgs args)
		{
			if (!string.IsNullOrEmpty(args.error))
			{
				RefDebug.Log(nameof(AppleSignInMethod), $"Error occured when tried to sign in with apple: {args.error}, state: {args.credentialState}");
				failCallback?.Invoke();
				return;
			}

			if (args.credentialState != UserCredentialState.Authorized)
			{
				RefDebug.Log(nameof(AppleSignInMethod), $"Credential state is invalid: {args.credentialState}");
				//failCallback?.Invoke();
				//return;
			}

			Credential credential = new Credential();
			credential.SetProviderId(ProviderId);
			credential.SetToken(args.userInfo.idToken);
			credential.SetRawNonce(rawNonce);
			credential.SetNonce(nonce);

			sucessCallback?.Invoke(credential);
		}

		private static string GenerateRandomString(int length)
		{
			if (length <= 0)
			{
				RefDebug.Error(nameof(AppleSignInMethod), "Expected nonce to have positive length");
				return null;
			}

			const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._";
			var cryptographicallySecureRandomNumberGenerator = new RNGCryptoServiceProvider();
			var result = string.Empty;
			var remainingLength = length;

			var randomNumberHolder = new byte[1];
			while (remainingLength > 0)
			{
				var randomNumbers = new List<int>(16);
				for (var randomNumberCount = 0; randomNumberCount < 16; randomNumberCount++)
				{
					cryptographicallySecureRandomNumberGenerator.GetBytes(randomNumberHolder);
					randomNumbers.Add(randomNumberHolder[0]);
				}

				for (var randomNumberIndex = 0; randomNumberIndex < randomNumbers.Count; randomNumberIndex++)
				{
					if (remainingLength == 0)
					{
						break;
					}

					var randomNumber = randomNumbers[randomNumberIndex];
					if (randomNumber < charset.Length)
					{
						result += charset[randomNumber];
						remainingLength--;
					}
				}
			}

			return result;
		}

		private static string GenerateSHA256NonceFromRawNonce(string rawNonce)
		{
			var sha = new SHA256Managed();
			var utf8RawNonce = Encoding.UTF8.GetBytes(rawNonce);
			var hash = sha.ComputeHash(utf8RawNonce);

			var result = string.Empty;
			for (var i = 0; i < hash.Length; i++)
			{
				result += hash[i].ToString("x2");
			}

			return result;
		}
	}
}

#endif
