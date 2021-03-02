namespace REF.Runtime.Notifications
{
	public class NotificationId
	{
		internal int AndroidId { get; private set; } = -1;
		internal string iOSId { get; private set; } = string.Empty;

		public NotificationId()
		{

		}

		public NotificationId(int id)
		{
			AndroidId = id;
		}

		public NotificationId(string id)
		{
			iOSId = id;
		}

		public void Invalidate()
		{
			AndroidId = -1;
			iOSId = string.Empty;
		}

		public override bool Equals(object obj)
		{
			if (obj is NotificationId)
			{
				var otherId = obj as NotificationId;
#if UNITY_ANDROID
				return AndroidId == otherId.AndroidId && AndroidId != -1;
#elif UNITY_IOS
				return iOSId == otherId.iOSId && !string.IsNullOrEmpty(iOSId);
#endif
				return false;
			}

			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public bool IsValid()
		{
			return AndroidId != -1 || !string.IsNullOrEmpty(iOSId);
		}
	}
}
