using Assets.Scripts.Gameplay;
using Assets.Scripts.Gameplay.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Buildings
{
    public class ResourceWatcher : MonoBehaviour
    {
        [SerializeField] private GameplayController gameplayController;
        [SerializeField] private CostsResources resourceCost;

        private Button button;

        public void Awake()
        {
            button = GetComponent<Button>();
        }

        public void Update()
        {
            button.interactable = gameplayController.CurrentResources >= resourceCost.Cost;
        }
    }
}
