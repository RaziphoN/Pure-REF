﻿using System.Linq;
using System.Collections.Generic;

namespace REF.Runtime.EventSystem.Bus
{
	public class EventBus : IEventBus
	{
		private readonly IDictionary<System.Type, IList<ISubscription>> subscriptions = new Dictionary<System.Type, IList<ISubscription>>();
		private readonly object lockingObj = new object();

		public string Subscribe<T>(System.Action<T> action) where T : IEventPayload
		{
			if (action == null)
			{
				return string.Empty;
			}

			lock (lockingObj)
			{
				var type = typeof(T);
				var id = System.Guid.NewGuid().ToString();
				var subscription = new Subscription<T>(action, id);

				if (!subscriptions.ContainsKey(type))
				{
					var actions = new List<ISubscription>();
					actions.Add(subscription);

					subscriptions.Add(type, actions);
				}
				else
				{
					var subs = subscriptions[type];
					var sameSubscription = subs?.SingleOrDefault((sub) =>
					{
						if (sub is Subscription<T>)
						{
							var tSub = (Subscription<T>)sub;
							if (tSub.Same(subscription))
							{
								return true;
							}
						}

						return false;
					});


					if (sameSubscription == null)
					{
						subs.Add(subscription);
					}
				}

				return id;
			}
		}

		public void Unsubscribe<T>(System.Action<T> action) where T : IEventPayload
		{
			if (action == null)
			{
				return;
			}

			lock (lockingObj)
			{
				var type = typeof(T);

				if (!subscriptions.ContainsKey(type))
				{
					return;
				}

				var subs = subscriptions[type];
				var foundSub = subs?.SingleOrDefault((sub) => 
				{
					var subT = sub as Subscription<T>;
					return subT.Same(action);
				});

				if (foundSub != null)
				{
					subs.Remove(foundSub);
				}
			}
		}

		public void Unsubscribe(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return;
			}

			lock (lockingObj)
			{
				var values = subscriptions.Values;
				
				foreach (var value in values)
				{
					var subscription = value?.SingleOrDefault((sub) => { return sub.GetId() == id; });
					if (subscription != null)
					{
						value.Remove(subscription);
						return;
					}
				}
			}
		}

		public void Invoke<T>(T ev) where T : IEventPayload
		{
			lock (lockingObj)
			{
				var type = typeof(T);
				
				if (subscriptions.ContainsKey(type))
				{
					var list = subscriptions[type];

					for (int idx = list.Count - 1; idx >= 0 && idx < list.Count; --idx)
					{
						var sub = list[idx];
						sub.Invoke(ev);
					}
				}
			}
		}
	}
}
