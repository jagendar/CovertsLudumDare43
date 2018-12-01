using Assets.Scripts.Gameplay.World;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    /// <summary>
    /// Borrowed/adapted from https://bitbucket.org/dandago/experimental/src/bd19cef6fb2e0709d10e10a2ad854fe9d8249066/AStarPathfinding/AStarPathfinding/Program.cs
    /// TODO: some optimizations could be had here by changing closed list to a data set with faster lookup for closed tiles - not sure how much of an issue that might be though
    /// </summary>
    public class VolcanoAStar
    {
        class Location
        {
            public int X;
            public int Y;
            public int totalCost;
            public int distanceFromStart;
            public int heuristic;
            public Location Parent;
        }

        /// <summary>
        /// Returns null to indicate that no path was found.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="world"></param>
        /// <returns></returns>
        public static List<Tile> GetPath(Vector2Int source, Vector2Int destination, World world)
        {
            List<Tile> steps = null;

            Location current = null;
            var start = new Location { X = source.x, Y = source.y };
            var target = new Location { X = destination.x, Y = destination.y };

            var openList = new List<Location>();
            var closedList = new List<Location>();

            int g = 0;
            bool found = false;

            // start by adding the original position to the open list
            openList.Add(start);

            while (openList.Count > 0)
            {
                // get the square with the lowest F score
                var lowest = openList.Min(l => l.totalCost);
                current = openList.First(l => l.totalCost == lowest);

                // add the current square to the closed list
                closedList.Add(current);
                
                // remove it from the open list
                openList.Remove(current);

                // if we added the destination to the closed list, we've found a path
                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                {
                    found = true;
                    break;
                }

                var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, world);
                g++;

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
                        adjacentSquare.distanceFromStart = g;
                        adjacentSquare.heuristic = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                        adjacentSquare.totalCost = adjacentSquare.distanceFromStart + adjacentSquare.heuristic;
                        adjacentSquare.Parent = current;

                        // and add it to the open list
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path
                        if (g + adjacentSquare.heuristic < adjacentSquare.totalCost)
                        {
                            adjacentSquare.distanceFromStart = g;
                            adjacentSquare.totalCost = adjacentSquare.distanceFromStart + adjacentSquare.heuristic;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }

            if(found)
            {
                steps = new List<Tile>();
                while(current != null)
                {
                    steps.Add(world[current.X, current.Y]);
                    current = current.Parent;
                }
                steps.Reverse();
            }
            return steps;
        }

        static IEnumerable<Location> GetWalkableAdjacentSquares(int x, int y, World map)
        {
            float tileHeight = map[x, y].Height;
            const float maxDeltaHeight = .5f;
            var proposedLocations = new List<Location>()
                    {
                        new Location { X = x, Y = y - 1 },
                        new Location { X = x, Y = y + 1 },
                        new Location { X = x - 1, Y = y },
                        new Location { X = x + 1, Y = y },
                    };

            return proposedLocations.Where(l => map[l.X,l.Y].IsWalkable && Mathf.Abs(map[l.X, l.Y].Height - tileHeight) < maxDeltaHeight);
        }

        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}