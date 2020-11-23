using UnityEngine;

namespace REF.Runtime.Online.Network.Response
{
	public class UnityJsonResponseConverter<T> : IResponseConverter<T> where T : IResponse
	{
		public T Convert(string message)
		{
			return JsonUtility.FromJson<T>(message);
		}
	}
}
