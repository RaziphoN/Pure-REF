﻿using UnityEngine.EventSystems;

namespace REF.Runtime.UI.Style
{
	public abstract class Style<T> where T : UIBehaviour
	{
		public abstract void Apply(T element);
	}
}
