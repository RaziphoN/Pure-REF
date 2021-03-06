﻿#if REF_ONLINE_AUTH

namespace REF.Runtime.Online.Auth.Method
{
	public interface ISignInMethod
	{
		void SignIn(System.Action<Credential> OnSuccess, System.Action OnFail);
		void SignOut();
	}
}

#endif
