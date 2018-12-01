using System;

namespace Assets.Scripts.Gameplay.Resources
{
    [Serializable]
    public struct ResourceCollection
    {
        public int Wood;
        public int Stone;
        public int Food;
        public int Population;

        public ResourceCollection(int wood = 0, int stone = 0, int food = 0, int population = 0)
        {
            Wood = wood;
            Stone = stone;
            Food = food;
            Population = population;
        }
    }
}
