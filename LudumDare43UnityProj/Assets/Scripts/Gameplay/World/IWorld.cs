using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    interface IWorld
    {
        ITile this[int x, int z] { get; set; }

        ITile this[Vector2Int pos] { get; set; }
    }
}
