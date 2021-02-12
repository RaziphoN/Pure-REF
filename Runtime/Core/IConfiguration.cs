namespace REF.Runtime.Core
{
	public interface IConfiguration
	{

	}

	public interface IConfigInjector
	{
		void Configure();
	}

	public class ConfigInjector<TConfig> : IConfigInjector where TConfig : IConfiguration
	{
		private IService<TConfig> service;
		private TConfig config;

		public ConfigInjector(IService<TConfig> service, TConfig config)
		{
			this.config = config;
			this.service = service;
		}

		public void Configure()
		{
			service.Configure(config);
		}
	}
}
