using REF.Runtime.Core;
using REF.Runtime.Serialization;

namespace REF.Runtime.Preference
{
	public interface IPreferenceServiceConfiguration : IConfiguration
	{
		ISaver GetSaver();
		ISerializer GetSerializer();
	}
}
