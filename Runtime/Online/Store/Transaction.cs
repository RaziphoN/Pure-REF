#if REF_STORE

using UnityEngine;

namespace REF.Runtime.Online.Store
{
	[System.Serializable]
	public class Transaction : ITransaction
	{
		[SerializeField] private string id;
		[SerializeField] private string providerId;
		[SerializeField] private string error;
		[SerializeField] private string receipt;
		[SerializeField] private IProduct product;
		[SerializeField] private PurchaseFailReason failReason = PurchaseFailReason.Unknown;

		public void SetId(string id)
		{
			this.id = id;
		}

		public void SetProviderId(string providerId)
		{
			this.providerId = providerId;
		}

		public void SetFailReason(PurchaseFailReason reason)
		{
			this.failReason = reason;
		}

		public void SetError(string error)
		{
			this.error = error;
		}

		public void SetProduct(IProduct product)
		{
			this.product = product;
		}

		public void SetReceipt(string receipt)
		{
			this.receipt = receipt;
		}

		public string GetError()
		{
			return error;
		}

		public string GetId()
		{
			return id;
		}

		public string GetProviderId()
		{
			return providerId;
		}

		public IProduct GetProduct()
		{
			return product;
		}

		public string GetReceipt()
		{
			return receipt;
		}

		public bool IsError()
		{
			return !string.IsNullOrEmpty(error);
		}

		public bool IsSuccessfull()
		{
			return !IsError();
		}

		public PurchaseFailReason GetFailReason()
		{
			return failReason;
		}
	}
}

#endif
