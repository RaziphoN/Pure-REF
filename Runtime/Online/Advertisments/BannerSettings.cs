#if REF_ONLINE_ADVERTISMENT

using UnityEngine;

namespace REF.Runtime.Online.Advertisments
{
	[System.Serializable]
	public class BannerSettings
	{
		public enum Position
		{
            Undefined = -1,
			Top = 0,
			Bottom = 1,
			TopLeft = 2,
			TopRight = 3,
			BottomLeft = 4,
			BottomRight = 5,
			Center = 6
		}

        public enum Orientation
        {
            Current = 0,
            Landscape = 1,
            Portrait = 2
        }

        public class Size
        {
            public enum Type
            {
                Standard = 0,
                SmartBanner = 1,
                AnchoredAdaptive = 2
            }

            private Type type;
            private Orientation orientation;
            private int width;
            private int height;

            public static readonly Size Banner = new Size(320, 50);
            public static readonly Size MediumRectangle = new Size(300, 250);
            public static readonly Size IABBanner = new Size(468, 60);
            public static readonly Size Leaderboard = new Size(728, 90);
            public static readonly Size SmartBanner = new Size(0, 0, Type.SmartBanner);
            public static readonly int FullWidth = -1;

            public Size(int width, int height)
            {
                this.type = Type.Standard;
                this.width = width;
                this.height = height;
                orientation = Orientation.Current;
            }

            private Size(int width, int height, Type type) : this(width, height)
            {
                this.type = type;
            }

            private static Size CreateAnchoredAdaptiveAdSize(int width, Orientation orientation)
            {
                Size adSize = new Size(width, 0, Type.AnchoredAdaptive);
                adSize.orientation = orientation;
                return adSize;
            }

            public static Size GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(int width)
            {
                return CreateAnchoredAdaptiveAdSize(width, Orientation.Landscape);
            }

            public static Size GetPortraitAnchoredAdaptiveBannerAdSizeWithWidth(int width)
            {
                return CreateAnchoredAdaptiveAdSize(width, Orientation.Portrait);
            }

            public static Size GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(int width)
            {
                return CreateAnchoredAdaptiveAdSize(width, Orientation.Current);
            }

            public int Width
            {
                get
                {
                    return width;
                }
            }

            public int Height
            {
                get
                {
                    return height;
                }
            }

            public Type AdType
            {
                get
                {
                    return type;
                }
            }

            public Orientation Orientation
            {
                get
                {
                    return orientation;
                }
            }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                    return false;

                Size other = (Size)obj;
                return (width == other.width) && (height == other.height)
                && (type == other.type) && (orientation == other.orientation);
            }

            public static bool operator ==(Size a, Size b)
            {
                if ((object)a == null)
                {
                    return (object)b == null;
                }

                return a.Equals(b);
            }

            public static bool operator !=(Size a, Size b)
            {
                if ((object)a == null)
                {
                    return (object)b != null;
                }

                return !a.Equals(b);
            }

            public override int GetHashCode()
            {
                int hashBase = 71;
                int hashMultiplier = 11;

                int hash = hashBase;
                hash = (hash * hashMultiplier) ^ width.GetHashCode();
                hash = (hash * hashMultiplier) ^ height.GetHashCode();
                hash = (hash * hashMultiplier) ^ type.GetHashCode();
                hash = (hash * hashMultiplier) ^ orientation.GetHashCode();
                return hash;
            }
        }

        private Position presetPosition = Position.Undefined;
        private Vector2Int position;
        private Size size = new Size(0, 0);

        public BannerSettings(Size size, Position position)
		{
            this.size = size;
            this.presetPosition = position;
		}

        public BannerSettings(Size size, int x, int y)
		{
            this.size = size;
            position.x = x;
            position.y = y;
		}

        public void SetSize(Size size)
		{
            this.size = size;
		}

        public Size GetSize()
		{
            return size;
		}

        public bool IsRelativePosition()
		{
            return presetPosition != Position.Undefined;
		}

        public void SetScreenPosition(int x, int y)
		{
            this.presetPosition = Position.Undefined;
            this.position.x = x;
            this.position.y = y;
		}

        public void SetRelativePosition(Position position)
		{
            this.position = Vector2Int.zero;
            presetPosition = position;
		}

        public Position GetRelativePosition()
		{
            return presetPosition;
		}

        public Vector2Int GetScreenPosition()
		{
            return position;
		}
    }
}

#endif