/* Author: Hunter Figgs.3 */

using Microsoft.Xna.Framework;
using System;
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

        public static Vector2 GetDirectionVector(char direction)
        {
            switch(direction)
            {
                case 'n':
                case 'N':
                    return directionVectorMap[CompassDirection.North];
                case 'e':
                case 'E':
                    return directionVectorMap[CompassDirection.East];
                case 's':
                case 'S':
                    return directionVectorMap[CompassDirection.South];
                case 'w':
                case 'W':
                    return directionVectorMap[CompassDirection.West];
                default:
                    throw new ArgumentException();
            }
        }

        public static CompassDirection GetCompassDirection(char direction)
        {
            switch (direction)
            {
                case 'n':
                case 'N':
                    return CompassDirection.North;
                case 'e':
                case 'E':
                    return CompassDirection.East;
                case 's':
                case 'S':
                    return CompassDirection.South;
                case 'w':
                case 'W':
                    return CompassDirection.West;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
