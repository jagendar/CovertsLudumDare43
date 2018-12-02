using Assets.Scripts.Gameplay.Resources;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int size = new Vector2Int(3, 3);

        [SerializeField]
        private ResourceCollection cost = new ResourceCollection(wood: 0, stone: 0, food: 0);

        [SerializeField]
        private Hologram constructionHologram = null;

        public ResourceCollection Cost
        {
            get { return cost; }
        }

        public Vector2Int Size
        {
            get { return size; }
        }

        public Hologram ConstructionHologram
        {
            get { return constructionHologram; }
        }

        public Vector2Int Position { get; internal set; }
    }
}
