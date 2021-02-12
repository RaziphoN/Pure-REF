using REF.Runtime.Core;

namespace REF.Runtime.Online.Service
{
	public class OnlineService<TConfig> : ServiceBase, IOnlineService<TConfig> where TConfig : IConfiguration
	{
		public virtual void Configure(TConfig config)
		{

		}
	}
}
