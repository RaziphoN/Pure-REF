#if REF_ONLINE_SOCIAL

namespace REF.Runtime.Online.Social
{
	public class SocialUserProfile : ISocialUserProfile
	{
		private string avatarUrl;
		private string displayName;
		private string userId;

		public void SetAvatarUrl(string avatarUrl)
		{
			this.avatarUrl = avatarUrl;
		}

		public void SetDisplayName(string displayName)
		{
			this.displayName = displayName;
		}

		public void SetUserId(string userId)
		{
			this.userId = userId;
		}

		public string GetAvatarUrl()
		{
			return avatarUrl;
		}

		public string GetDisplayName()
		{
			return displayName;
		}

		public string GetUserId()
		{
			return userId;
		}
	}
}

#endif
