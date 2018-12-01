namespace Assets.Scripts.Gameplay.World
{
    class World : IWorld
    {
        public World(Tile[,] tiles)
        {
            this.tiles = tiles;
        }

        private readonly Tile[,] tiles;

        public Tile this[int x, int z]
        {
            get { return tiles[x, z]; }
            set { tiles[x, z] = value;  }
        }
    }
}
