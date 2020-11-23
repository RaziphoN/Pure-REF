namespace REF.Runtime.Online.Network.Response
{
	public interface IResponseConverter<T> where T : IResponse
	{
		T Convert(string message);
	}
}
