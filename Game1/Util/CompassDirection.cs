using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game1.Util
{
    public enum CompassDirection
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public static class CompassDirectionUtil
    {
        private static Dictionary<CompassDirection, Vector2> directionVectorMap = new Dictionary<CompassDirection, Vector2>()
        {
            { CompassDirection.North, new Vector2(0, -1) },
            { CompassDirection.East, new Vector2(1, 0) },
            { CompassDirection.South, new Vector2(0, 1) },
            { CompassDirection.West, new Vector2(-1, 0) }
        };

        public static Vector2 GetDirectionVector(CompassDirection direction)
        {
            return directionVectorMap[direction];
        }
    }
}
