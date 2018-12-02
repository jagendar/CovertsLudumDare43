using Assets.Scripts.Gameplay.Resources;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    public class Building : CostsResources
    {
        [SerializeField]
        private Vector2Int size = new Vector2Int(3, 3);

        [SerializeField]
        private Hologram constructionHologram = null;

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
