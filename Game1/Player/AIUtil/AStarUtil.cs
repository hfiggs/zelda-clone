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
        public Rectangle R;
        public int F;
        public int G;
        public int H;
        public Location Parent;
    }

    class Program
{
    public static Stack<int> findNextDecision(Rectangle startingPosition, Rectangle finish, List<Rectangle> Obstacles)
    {

        Location current = null;
           Location best = null;
        var start = new Location {R = startingPosition, G = 0};
        var target = new Location {R = finish};
        var openList = new List<Location>();
        var closedList = new List<Location>();
        int g = 0;
            int numberOfInstructions = (int)(.25 * Math.Sqrt(Math.Pow(finish.X - startingPosition.X, 2) + Math.Pow(finish.Y - startingPosition.Y, 2)));
        const int runTimer = 600;
        int iteration = 0;
        // start by adding the original position to the open list
        openList.Add(start);
            best = new Location { R = startingPosition, G = 0, H = ComputeHScore(startingPosition, finish), F = 0, Parent = null };
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
                if (closedList.FirstOrDefault(l => l.R.Intersects(target.R) && l.R.Intersects(target.R)) != null)
                {
                    best = current;
                    break;
                }
            var adjacentSquares = GetWalkableAdjacentSquares(current.R, Obstacles);
            g += 1;

            foreach (var adjacentSquare in adjacentSquares)
            {
                // if this adjacent square is already in the closed list, ignore it
                if (closedList.FirstOrDefault(l => l.R.Equals(adjacentSquare.R)) != null)
                    continue;

                // if it's not in the open list...
                if (openList.FirstOrDefault(l => l.R.Equals(adjacentSquare.R)) == null)
                {
                    // compute its score, set the parent
                    adjacentSquare.G = g;
                    adjacentSquare.H = ComputeHScore(adjacentSquare.R, target.R);
                    adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                    adjacentSquare.Parent = current;
                        if (adjacentSquare.H <= best.H && adjacentSquare.G >= best.G)
                            best = adjacentSquare;
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
                            if (adjacentSquare.H <= best.H && adjacentSquare.G >= best.G)
                                best = adjacentSquare;
                        }
                }

            }
        }

            // assume path was found; let's show it
            Stack<int> Directions = new Stack<int>();
            int direction = -5;
        while (best.Parent != null)
        {
                if (best.G <= numberOfInstructions)
                {
                    if (best.R.X - best.Parent.R.X != 0)
                    {
                        direction = best.R.X - best.Parent.R.X;

                    }
                    else
                        direction = best.R.Y - best.Parent.R.Y + 3;
                    Directions.Push(direction);
                }
            best = best.Parent;
        }
        return Directions;
    }

    static List<Location> GetWalkableAdjacentSquares(Rectangle current, List<Rectangle> map)
    {
        List<Location> possibleDirections = new List<Location>();
        var proposedLocations = new List<Location>()
            {
                new Location { R = new Rectangle(current.X - 2, current.Y, current.Width, current.Height) },
                new Location { R = new Rectangle(current.X + 2, current.Y, current.Width, current.Height) },
                new Location {R = new Rectangle(current.X, current.Y - 2, current.Width, current.Height)},
                new Location { R = new Rectangle(current.X, current.Y + 2, current.Width, current.Height) },
            };

        foreach (Location L in proposedLocations)
        {
            bool hitsObstacle = false;
            foreach (Rectangle R in map)
            {
                if (R.Intersects(L.R))
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

    static int ComputeHScore(Rectangle R, Rectangle targetR)
    {
            int H = 0;
                H = Math.Abs(targetR.Center.X - R.Center.X) + Math.Abs(targetR.Center.Y - R.Center.Y);
            return H;
    }
}
}