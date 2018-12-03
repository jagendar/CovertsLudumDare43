using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class WorldObjectHightlight : MonoBehaviour
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

        public void OnDestroy()
        {
            Destroy(highlightInstance);
        }
    }
}