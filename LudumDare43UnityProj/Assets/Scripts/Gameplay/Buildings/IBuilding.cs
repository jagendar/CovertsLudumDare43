using Assets.Scripts.Gameplay.Resources;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    interface IBuilding
    {
        Vector2Int Size { get; }

        Vector2Int Pivot { get; }
        
        ResourceCost Cost { get; }
    }
}
