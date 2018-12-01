using System;

namespace Assets.Scripts.Gameplay.Resources
{
    [Serializable]
    struct ResourceCost
    {
        public int Wood;
        public int Stone;
        public int Food;

        public ResourceCost(int wood = 0, int stone = 0, int food = 0)
        {
            Wood = wood;
            Stone = stone;
            Food = food;
        }
    }
}
