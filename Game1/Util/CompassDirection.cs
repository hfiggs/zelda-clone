﻿/* Author: Hunter Figgs.3 */

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
        West = 3,
        None = 4
    }

    public static class CompassDirectionUtil
    {
        private const char North = 'N', South = 'S', West = 'W', East = 'E';
        private const char north = 'n', south = 's', west = 'w', east = 'e';

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
                case north:
                case North:
                    return directionVectorMap[CompassDirection.North];
                case east:
                case East:
                    return directionVectorMap[CompassDirection.East];
                case south:
                case South:
                    return directionVectorMap[CompassDirection.South];
                case west:
                case West:
                    return directionVectorMap[CompassDirection.West];
                default:
                    throw new ArgumentException();
            }
        }

        public static CompassDirection GetCompassDirection(char direction)
        {
            switch (direction)
            {
                case north:
                case North:
                    return CompassDirection.North;
                case east:
                case East:
                    return CompassDirection.East;
                case south:
                case South:
                    return CompassDirection.South;
                case west:
                case West:
                    return CompassDirection.West;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
