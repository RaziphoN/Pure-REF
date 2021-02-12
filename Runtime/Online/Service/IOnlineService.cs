using REF.Runtime.Core;

namespace REF.Runtime.Online
{
	public interface IOnlineService<TConfig> : IService<TConfig> where TConfig : IConfiguration
	{
	}
}
