namespace REF.Runtime.EventSystem.Bus
{
	public interface ISubscription
	{
		string GetId();
		void Invoke(IEventPayload ev);
	}
}
