namespace Assets.Scripts.Gameplay.World
{
    interface IWorld
    {
        ITile this[int x, int z] { get; set; }
    }
}
