using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class World : IWorld
    {
        public World(int width, int height)
        {
            this.tiles = new Tile[width, height];
        }

        private readonly Tile[,] tiles;

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
    }
}
