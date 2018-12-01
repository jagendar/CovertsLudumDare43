namespace Assets.Scripts.Gameplay.World
{
    interface ITile
    {
        bool IsBuildable { get; }

        float Height { get; }
    }
}
