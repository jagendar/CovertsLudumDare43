using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    class World : IWorld
    {
        public World(ITile[][] tiles)
        {
            this.tiles = tiles;
        }

        private readonly ITile[][] tiles;

        public ITile this[int x, int z]
        {
            get { return tiles[x][z]; }
            set { tiles[x][z] = value;  }
        }

        public ITile this[Vector2Int pos]
        {
            get { return this[pos.x, pos.y]; }
            set { this[pos.x, pos.y] = value; }
        }
    }
}
