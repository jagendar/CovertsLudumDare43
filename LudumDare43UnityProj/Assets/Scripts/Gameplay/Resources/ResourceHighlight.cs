using UnityEngine;

namespace Assets.Scripts.Gameplay.Resources
{
    public class ResourceHighlight : MonoBehaviour
    {
        [SerializeField] private GameObject highlightTemplate;

        private GameObject highlightInstance;

        public bool IsHighlighted { get; set; }

        public void Awake()
        {
            highlightInstance = Instantiate(highlightTemplate, transform.position, Quaternion.identity);
        }

        public void Update()
        {
            highlightInstance.SetActive(IsHighlighted);
        }
    }
}