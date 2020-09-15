#if REF_ONLINE_REMOTE_CONFIG

using System.Collections.Generic;

namespace REF.Runtime.Online.RemoteConfig
{
	public interface IConfigProvider
	{
		bool HasKey(string key);
		Value GetValue(string key);

		IEnumerable<string> GetKeys();
		IDictionary<string, Value> GetData();
	}
}

#endif
