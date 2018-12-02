using System.Collections.Generic;
using Assets.Scripts.Gameplay.Buildings;
using Assets.Scripts.Gameplay.Resources;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class World
    {
        private readonly Tile[,] tiles;
        private readonly List<Building> buildings;
        private GameplayController gameplayController = GameplayController.instance;

        public int Width { get { return tiles.GetLength(0); } }
        public int Height { get { return tiles.GetLength(1); } }

        public World(int width, int height)
        {
            tiles = new Tile[width, height];
            buildings = new List<Building>();
        }

        private IEnumerable<Building> Buildings
        {
            get { return buildings; }
        }

        public Tile this[int x, int z]
        {
            get { return tiles[x, z]; }
            set { tiles[x, z] = value;  }
        }

        public Tile this[Vector2Int pos]
        {
            get { return this[pos.x, pos.y]; }
            set { this[pos.x, pos.y] = value; }
        }

        public Building CreateNewBuilding(Building building, Vector2Int position)
        {
            var buildingInstance = Object.Instantiate(building, this[position].transform.position, Quaternion.identity);

            var tilesUnder = Util.PositionsUnderBuilding(position, buildingInstance);
            foreach (var tilePosition in tilesUnder)
            {
                var tile = this[tilePosition];
                tile.IsBuildable = false;
            }

            gameplayController.CurrentResources -= building.Cost;

            buildings.Add(buildingInstance);
            return buildingInstance;
        }
    }
}
