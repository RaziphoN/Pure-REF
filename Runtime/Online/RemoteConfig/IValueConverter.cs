
namespace REF.Runtime.Online.RemoteConfig
{
	public interface IValueConverter<T>
	{
		T Convert(Value value);
	}
}
