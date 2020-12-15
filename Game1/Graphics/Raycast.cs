using Game1.Environment;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game1.Graphics
{
    public static class Raycast
    {
        public static List<Rectangle> GetHitboxes(Room room)
        {
            var hitboxList = new List<Rectangle>();


            //foreach (IEnvironment environment in DetectionUtil.GetSingleCollisionObjects(room.InteractEnviornment))
            foreach (IEnvironment environment in room.InteractEnviornment)
            {
                hitboxList.AddRange(environment.GetHitboxes());
            }

            // TOOD: Consider adding other hitboxes (such as enemies)

            return hitboxList;
        }

        public static List<Line> GetRaycastLines(Screen screen)
        {
            var rectList = GetHitboxes(screen.CurrentRoom);
                
            var centerPoint = Vector2.Add(screen.Players.First().GetPlayerHitbox().Location.ToVector2(), new Vector2(7.5f, 5f));

            var angleOffset = 0.001f;

            var lineList = new List<Line>();

            foreach (Rectangle rect in rectList)
            {
                foreach (Vector2 vertex in GetRectVerts(rect))
                {
                    var line = new Line(centerPoint.X, centerPoint.Y, vertex.X, vertex.Y);

                    lineList.Add(GetRayCastLine(rectList, new Vector2(line.X1, line.Y1), new Vector2(line.X2, line.Y2)));

                    line.Elongate();

                    line.Rotate(angleOffset);

                    lineList.Add(GetRayCastLine(rectList, new Vector2(line.X1, line.Y1), new Vector2(line.X2, line.Y2)));

                    line.Rotate(-2 * angleOffset);

                    lineList.Add(GetRayCastLine(rectList, new Vector2(line.X1, line.Y1), new Vector2(line.X2, line.Y2)));
                }
            }

            return lineList;
        }

        private static Line GetRayCastLine(List<Rectangle> rectList, Vector2 centerPoint, Vector2 vertex)
        {
            var line = new Line(centerPoint.X, centerPoint.Y, vertex.X, vertex.Y);

            var shortestLine = new Line(centerPoint.X, centerPoint.Y, float.MaxValue, float.MaxValue);

            foreach (Rectangle rect in rectList)
            {
                foreach (Line wall in GetRectLines(rect))
                {
                    var x1 = wall.X1;
                    var y1 = wall.Y1;
                    var x2 = wall.X2;
                    var y2 = wall.Y2;

                    var x3 = line.X1;
                    var y3 = line.Y1;
                    var x4 = line.X2;
                    var y4 = line.Y2;

                    var den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

                    if (Math.Abs(den) < 0.000001)
                    {
                        continue;
                    }

                    var t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;

                    var u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;


                    if (t > 0f && t < 1.0f && u > 0f)
                    {
                        var pointX = x1 + t * (x2 - x1);

                        var pointY = y1 + t * (y2 - y1);

                        var newLine = new Line(centerPoint.X, centerPoint.Y, pointX, pointY);

                        if (newLine.GetLength() < line.GetLength() && newLine.GetLength() < shortestLine.GetLength())
                        {
                            shortestLine = newLine;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (shortestLine.X2 == float.MaxValue && shortestLine.Y2 == float.MaxValue)
                return new Line(line.X1, line.Y1, line.X2, line.Y2);

            return shortestLine;
        }

        private static List<Vector2> GetRectVerts(Rectangle rect)
        {
            var location = rect.Location.ToVector2();

            var width = rect.Width;
            var height = rect.Height;

            return new List<Vector2>()
            {
                Vector2.Add(location, new Vector2(0, 0)),

                Vector2.Add(location, new Vector2(width, 0)),

                Vector2.Add(location, new Vector2(0, height)),

                Vector2.Add(location, new Vector2(width, height))
            };
        }

        private static List<Line> GetRectLines(Rectangle rect)
        {
            var location = rect.Location.ToVector2();

            var width = rect.Width;
            var height = rect.Height;

            return new List<Line>()
            {
                // Four sides of rectangle
                new Line(location.X, location.Y, location.X + width, location.Y + 0),

                new Line(location.X, location.Y, location.X, location.Y + height),

                new Line(location.X, location.Y + height, location.X + width, location.Y + height),

                new Line(location.X + width, location.Y, location.X + width, location.Y + height),

                // Extra lines to stop rogue rays
                new Line(location.X + 1, location.Y, location.X + 1, location.Y + height),

                new Line(location.X + width - 1, location.Y, location.X + width - 1, location.Y + height),

                new Line(location.X, location.Y + 1, location.X + width, location.Y + 1),

                new Line(location.X, location.Y + height - 1, location.X + width, location.Y + height - 1)
            };
        }
    }
}
