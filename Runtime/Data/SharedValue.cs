using UnityEngine;

namespace REF.Runtime.Data
{
	[System.Serializable]
	public class SharedValue<T> : ScriptableObject
	{
#if UNITY_EDITOR
		private T _saved;
#endif
		[SerializeField] protected T _value;

		public virtual T Value
		{
			get { return _value; }
			set
			{
				if (!_value.Equals(value))
					_value = value;
			}
		}

#if UNITY_EDITOR
		protected virtual void Restore()
		{
			_value = _saved;
		}
#endif

#if UNITY_EDITOR
		protected virtual void Save()
		{
			_saved = _value;
		}
#endif

#if UNITY_EDITOR
		private void OnValidate()
		{
			Save();
		}
#endif

#if UNITY_EDITOR
		private void OnDisable()
		{
			Restore();
		}
#endif
	}
}
