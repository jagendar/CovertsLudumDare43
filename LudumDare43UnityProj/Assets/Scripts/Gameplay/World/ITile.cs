namespace Assets.Scripts.Gameplay.World
{
    interface ITile
    {
        bool IsBuildable { get; }

        int Height { get; }
    }
}
