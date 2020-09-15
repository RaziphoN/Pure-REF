#if REF_ONLINE_ANALYTICS

namespace REF.Runtime.Online.Analytics
{
	public class Parameter
	{
		public Parameter(string name, Value value)
		{
			this.Name = name;
			this.Value = value;
		}

		public string Name;
		public Value Value;
	}
}

#endif
