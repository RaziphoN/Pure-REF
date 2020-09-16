
namespace REF.Runtime.EventSystem.Bus
{
	public interface IEventBus
	{
		string Subscribe<T>(System.Action<T> ev) where T : IEventPayload;
		void Unsubscribe<T>(System.Action<T> ev) where T : IEventPayload;
		void Unsubscribe(string id);

		void Invoke<T>(T ev) where T : IEventPayload;
	}
}
