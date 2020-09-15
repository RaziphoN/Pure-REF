#if REF_ONLINE_REMOTE_CONFIG

namespace REF.Runtime.Online.RemoteConfig
{
	public interface IConfigModifier
	{
		void AddValue(string key, Value value);
		void RemoveValue(string key);

		void SetValue(string key, Value value);
	}
}

#endif
