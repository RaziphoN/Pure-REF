using System.Collections.Generic;

namespace REF.Runtime.Online.Social.Facebook
{
	[System.Serializable]
	internal class Friends
	{
		User[] data;

		public IList<ISocialUserProfile> ToSocialProfiles()
		{
			SocialUserProfile[] friends = new SocialUserProfile[data.Length];

			for (int idx = 0; idx < data.Length; ++idx)
			{
				var user = data[idx];

				var profile = new SocialUserProfile();
				profile.SetUserId(user.id);
				profile.SetAvatarUrl(user.picture.data.url);
				profile.SetDisplayName(user.name);

				friends[idx] = profile;
			}

			return friends;
		}
	}

	[System.Serializable]
	internal class User
	{
		public string id;
		public string name;
		public Picture picture;

		public ISocialUserProfile ToSocialProfile()
		{
			var profile = new SocialUserProfile();
			profile.SetUserId(id);
			profile.SetAvatarUrl(picture.data.url);
			profile.SetDisplayName(name);

			return profile;
		}
	}

	[System.Serializable]
	internal class Picture
	{
		[System.Serializable]
		public class Data
		{
			public string url;
			public int width;
			public int height;
		}

		public Data data;
	}
}
