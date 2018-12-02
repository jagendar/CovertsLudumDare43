using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gameplay.World;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    class Util
    {
        public static Vector2Int[] PositionsUnderBuilding(Vector2Int targetPosition, Building building)
        {
            var toCheck = new List<Vector2Int>();

            for (var x = 0; x < building.Size.x; x++)
            {
                for (var z = 0; z < building.Size.y; z++)
                {
                    toCheck.Add(new Vector2Int(targetPosition.x + x - building.Size.x / 2, targetPosition.y + z - building.Size.y / 2));
                }
            }

            return toCheck.ToArray();
        }

        /// <summary>
        /// Checks whether or not a given building can be built at the given cursor location.
        /// </summary>
        /// <param name="world">The world to build in.</param>
        /// <param name="position">The position build at (will be adjusted by the buildings pivot).</param>
        /// <param name="building">The building to be built.</param>
        /// <returns>True if the position is a valid build location.</returns>
        public static bool CanBuildAt(World.World world, Vector2Int position, Building building)
        {
            var toCheck = PositionsUnderBuilding(position, building);

            foreach(var val in toCheck)
            {
                if(val.x < 0 || val.y < 0 || val.x > world.Width || val.y > world.Height)
                {
                    return false;
                }
            }

            var validHeight = world[position].Height;

            return toCheck.All(pos => world[pos].IsBuildable && Math.Abs(world[pos].Height - validHeight) < float.Epsilon);
        }

        internal static IEnumerable<Vector2Int> PositionsUnderBuilding(Building building)
        {
            return PositionsUnderBuilding(new Vector2Int(Mathf.RoundToInt(building.transform.position.x), Mathf.RoundToInt(building.transform.position.y)), building);
        }
    }
}
