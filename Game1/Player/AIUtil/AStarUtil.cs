using Game1.Enemy;
using Game1.Environment;
using Game1.RoomLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This code is based off code found at: https://gigi.nullneuron.net/gigilabs/a-pathfinding-example-in-c/

namespace AStarPathfinding
{
    class Location
    {
        public int X;
        public int width;
        public int height;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Location Parent;
    }

    class Program
{
    public static int findNextDecision(Rectangle startingPosition, Rectangle finish, List<Rectangle> Obstacles)
    {

        Location current = null;
        var start = new Location { X = startingPosition.X, Y = startingPosition.Y, width = startingPosition.Width, height = startingPosition.Height };
        var target = new Location { X = finish.X, Y = finish.Y, width = finish.Width, height = finish.Height };
        var openList = new List<Location>();
        var closedList = new List<Location>();
        int g = 0;
        const int runTimer = 50;
        int iteration = 0;
        // start by adding the original position to the open list
        openList.Add(start);

        while (openList.Count > 0 && iteration != runTimer)
        {
            iteration = iteration + 1;
            // get the square with the lowest F score
            var lowest = openList.Min(l => l.F);
            current = openList.First(l => l.F == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                break;

            var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, current.width, current.height, Obstacles);
            g += 1;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;

                    // and add it to the open list
                    openList.Insert(0, adjacentSquare);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score
                    // lower, if yes update the parent because it means it's a better path
                    if (g + adjacentSquare.H < adjacentSquare.F)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;
                    }
                }
            }
        }

        // assume path was found; let's show it
        int direction = -5;
        while (current.Parent != null)
        {

            if (current.Parent.Parent == null)
            {
                if (current.X - current.Parent.X != 0)
                {
                    direction = current.X - current.Parent.X;
                }
                else
                    direction = current.Y - current.Parent.Y + 3;
            }
            current = current.Parent;
        }
        return direction;
    }

    static List<Location> GetWalkableAdjacentSquares(int x, int y, int width, int height, List<Rectangle> map)
    {
        List<Location> possibleDirections = new List<Location>();
        var proposedLocations = new List<Location>()
            {
                new Location { X = x, Y = y - 1, height = height, width = width},
                new Location { X = x, Y = y + 1, height = height, width = width },
                new Location { X = x - 1, Y = y, height = height, width = width },
                new Location { X = x + 1, Y = y, height = height, width = width },
            };

        foreach (Location L in proposedLocations)
        {
            Rectangle P = new Rectangle(L.X, L.Y, L.width, L.height);
            bool hitsObstacle = false;
            foreach (Rectangle R in map)
            {
                if (R.Intersects(P))
                {
                    hitsObstacle = true;
                }
            }
            if (!hitsObstacle)
            {
                possibleDirections.Add(L);
            }
        }

        return possibleDirections;
    }

    static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Math.Abs(targetX - x) + Math.Abs(targetY - y);
    }
}
}