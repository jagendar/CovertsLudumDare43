using UnityEngine;

namespace Assets.Scripts.Gameplay.Resources
{
    public class CollectableResource : MonoBehaviour
    {
        [SerializeField] private uint amount;

        [SerializeField] private ResourceType resourceType;

        public uint Amount
        {
            get { return amount; }
            set { amount = value; }
        }

        public ResourceType ResourceType
        {
            get { return resourceType; }
            set { resourceType = value; }
        }
    }
}
