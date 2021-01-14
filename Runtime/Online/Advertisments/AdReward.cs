#if REF_ONLINE_ADVERTISMENT

namespace REF.Runtime.Online.Advertisments
{
	public class AdReward
	{
		public string Type { get; private set; }
		public double Amount { get; private set; }

		public AdReward(string type, double count)
		{
			Type = type;
			Amount = count;
		}
	}
}

#endif