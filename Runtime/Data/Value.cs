using UnityEngine;

namespace REF.Runtime.Data
{
	[System.Serializable]
	public abstract class Value<T>
	{
		[SerializeField] private ValueType _type = ValueType.Const;
		[SerializeField] private T _const = default(T);
		protected abstract SharedValue<T> _shared { get; }

		public T Data
		{
			set
			{
				switch (_type)
				{
					case ValueType.Const:
					{
						if (!_const.Equals(value))
							_const = value;
					}
					break;

					case ValueType.Shared:
					{
						if (!_shared.Equals(value))
							_shared.Value = value;
					}
					break;
				}
			}

			get
			{
				switch (_type)
				{
					case ValueType.Const:
						return _const;

					case ValueType.Shared:
						return _shared.Value;

					default:
						Debug.Log("[Value] - unknown type: " + _type);
						return default(T);
				}
			}
		}
	}
}
