#if REF_ONLINE_REMOTE_CONFIG

namespace REF.Runtime.Online.RemoteConfig
{
	public interface IValueConverter<T>
	{
		T Convert(Value value);
	}
}

#endif
