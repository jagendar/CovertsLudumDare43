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
    }
}
