using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    interface IWorld
    {
        Tile this[int x, int z] { get; set; }

        Tile this[Vector2Int pos] { get; set; }
    }
}
