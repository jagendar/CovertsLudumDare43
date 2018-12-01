using Assets.Scripts.Gameplay.Resources;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    interface IBuilding
    {
        Vector2 Size { get; }
        
        ResourceCost Cost { get; }
    }
}
