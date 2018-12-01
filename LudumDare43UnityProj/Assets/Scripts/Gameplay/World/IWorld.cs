namespace Assets.Scripts.Gameplay.World
{
    interface IWorld
    {
        Tile this[int x, int z] { get; set; }
    }
}
