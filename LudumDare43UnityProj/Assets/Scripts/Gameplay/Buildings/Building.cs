using Assets.Scripts.Gameplay.Resources;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    class Building : MonoBehaviour, IBuilding
    {
        [SerializeField]
        private readonly Vector2 size = new Vector2(3, 3);

        [SerializeField]
        private readonly ResourceCost cost = new ResourceCost(wood: 0, stone: 0, food: 0);

        public ResourceCost Cost
        {
            get { return cost; }
        }

        public Vector2 Size
        {
            get { return size; }
        }
    }
}
