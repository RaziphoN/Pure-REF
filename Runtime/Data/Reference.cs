using UnityEngine;

namespace REF.Runtime.Data
{
	public enum ReferenceType
	{
		Constant = 0,
		Reference = 1,
	}

	[System.Serializable]
	public abstract class Reference<T, VariableT> where VariableT : Variable<T>
	{
		[SerializeField] private ReferenceType type = ReferenceType.Constant;
		[SerializeField] private VariableT reference;
		[SerializeField] private T constant;

		public Reference() { }

		public Reference(T value) { Const = value; }

		public ReferenceType Type
		{
			get { return type; }
			set { type = value; }
		}

		public VariableT Ref
		{
			get { return reference; }
			set { reference = value; }
		}

		public T Const
		{
			get { return constant; }
			set { constant = value; }
		}


		public T Value
		{
			get
			{
				switch (type)
				{
					case ReferenceType.Reference:
					{
						return reference.Value;
					}

					case ReferenceType.Constant:
					{
						return constant;
					}

					default:
					{
						Debug.LogError($"[Reference] - Unknown reference type: {type.ToString()}");
						return default(T);
					}
				}
			}

			set
			{
				switch (type)
				{
					case ReferenceType.Reference:
					{
						reference.Value = value;
					}
					break;

					case ReferenceType.Constant:
					{
						constant = value;
					}
					break;
				}
			}
		}
	}
}
