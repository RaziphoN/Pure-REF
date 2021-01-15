#if REF_ONLINE_ADVERTISMENT

using UnityEngine;

namespace REF.Runtime.Online.Advertisments
{
	[System.Serializable]
	public class BannerSettings
	{
        public enum Type
        {
            Preset, // Will choose the closest preset from Advertisment service available size presets from developer guide
            Smart, // width wouldn't matter, it would always fit screen width, but height would be choosen automatically from closest preset or by ad service
            Adaptive, // height woudn't matter here, but service would try to request precise width for you ad size or adapt it to requested one. Height is adapted automatically
            Custom // just send custom size banner request, it would use "Preset" policy if custom size banners is not supported by ad service
        }

        public enum PositionPolicy
		{
            Defined, // Took value from Position enum
            Custom // Took custom position if supported, otherwise enum value
		}

        public enum Position
		{
			Top,
			Bottom,
			TopLeft,
			TopRight,
			BottomLeft,
			BottomRight,
			Center
		}

        private Type sizePolicy = Type.Preset;
        private PositionPolicy posPolicy = PositionPolicy.Defined;

        private Position enumPos = Position.Bottom;
        private Vector2Int position = new Vector2Int(0, 0);
        private Vector2Int size = new Vector2Int(0, 0);

        public BannerSettings()
		{

		}

        public void SetBannerType(Type type)
		{
            this.sizePolicy = type;
		}

        public void SetSize(int width, int height)
		{
            size.x = width;
            size.y = height;
		}

        public void SetPosition(int x, int y)
        {
            this.posPolicy = PositionPolicy.Custom;

            this.position.x = x;
            this.position.y = y;
        }

        public void SetPosition(Position position)
        {
            this.posPolicy = PositionPolicy.Defined;
            this.enumPos = position;
            this.position = Vector2Int.zero;
        }

        internal Position GetRelativePosition()
		{
            return enumPos;
		}

        internal Vector2Int GetSize()
		{
            return size;
		}

        internal Vector2Int GetPosition()
		{
            return position;
		}

        internal Type GetBannerType()
		{
            return sizePolicy;
		}

        internal PositionPolicy GetPositionType()
		{
            return posPolicy;
		}
    }
}

#endif