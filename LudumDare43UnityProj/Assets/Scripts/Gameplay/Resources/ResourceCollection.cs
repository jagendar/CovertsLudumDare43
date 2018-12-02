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

        public static bool operator >=(ResourceCollection a, ResourceCollection b)
        {
            return a.Wood >= b.Wood &&
                   a.Stone >= b.Stone &&
                   a.Food >= b.Food &&
                   a.Population >= b.Population;
        }

        public static bool operator <=(ResourceCollection a, ResourceCollection b)
        {
            return a.Wood <= b.Wood &&
                   a.Stone <= b.Stone &&
                   a.Food <= b.Food &&
                   a.Population <= b.Population;
        }

        public static ResourceCollection operator -(ResourceCollection a, ResourceCollection b)
        {
            return new ResourceCollection
            {
                Wood = a.Wood - b.Wood,
                Stone = a.Stone - b.Stone,
                Food = a.Food - b.Food,
                Population = a.Population - b.Population,
            };
        }
    }
}
