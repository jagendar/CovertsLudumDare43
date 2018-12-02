using Assets.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Buildings
{
    public class SelectedBuildingCommands : MonoBehaviour
    {
        [SerializeField] private Button confirmYesButton;
        [SerializeField] private Button confirmNoButton;
        [SerializeField] private Button destroyButton;

        [SerializeField] private Vector2 offset = new Vector2(0, 0);

        private bool needsConfirmation = false;
        private GameplayController controller;

        public void Start()
        {
            controller = GameplayController.instance;
        }

        public void Update()
        {
            bool showConfirms = controller.SelectedBuilding && needsConfirmation;
            confirmYesButton.gameObject.SetActive(showConfirms);
            confirmNoButton.gameObject.SetActive(showConfirms);

            bool showDestroy = controller.SelectedBuilding && needsConfirmation;
            destroyButton.gameObject.SetActive(showDestroy);

            if (!controller.SelectedBuilding) return;

            var targetPosition = controller.SelectedBuilding.transform.position;
            var targetScreenPosition = Camera.main.WorldToScreenPoint(targetPosition);

            gameObject.transform.position = targetScreenPosition + new Vector3(offset.x, 0, offset.y);
        }
    }
}
