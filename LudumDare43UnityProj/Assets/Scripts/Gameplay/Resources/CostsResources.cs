using UnityEngine;

namespace Assets.Scripts.Gameplay.Resources
{
    public abstract class CostsResources : MonoBehaviour
    {
        [SerializeField]
        protected ResourceCollection cost;

        public ResourceCollection Cost
        {
            get { return cost; }
        }
    }
}