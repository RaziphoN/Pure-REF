using UnityEngine;

namespace REF.Runtime.Utilities.Serializable.Nullable
{
	[System.Serializable]
	public class SerializableNullable<T> where T : struct
	{
		[SerializeField] bool hasValue = false;
		[SerializeField] T value = default(T);

		public bool HasValue { get { return hasValue; } }

		public T? Value
		{
			get
			{
				if (HasValue)
					return value;

				return null;
			}

			set
			{
				if (value == null)
				{
					hasValue = false;
					this.value = default(T);
				}
				else
				{
					hasValue = true;
					this.value = value.Value;
				}
			}
		}
	}
}
