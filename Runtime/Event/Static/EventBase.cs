﻿using UnityEngine;

namespace REF.Runtime.EventSystem.Static
{
	// this class must be a base class for generic events to make a custom editor that is common for all of them
	public abstract class EventBase : ScriptableObject
	{
		public abstract void Invoke();
	}
}
