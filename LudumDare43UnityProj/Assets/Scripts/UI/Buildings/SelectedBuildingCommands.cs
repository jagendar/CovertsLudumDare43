using Assets.Scripts.Gameplay;
using Assets.Scripts.Gameplay.World;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Buildings
{
    public class SelectedBuildingCommands : MonoBehaviour
    {
        [SerializeField] private Button confirmYesButton;
        [SerializeField] private Button confirmNoButton;
        [SerializeField] private Button destroyButton;
        [SerializeField] private RectTransform buttonsContainer;

        [SerializeField] private Vector2 offset = new Vector2(0, 0);

        private bool needsConfirmation = false;
        private GameplayController controller;

        public void Start()
        {
            controller = GameplayController.instance;
            destroyButton.onClick.AddListener(OnDestroy);
            confirmYesButton.onClick.AddListener(OnConfirmYes);
            confirmNoButton.onClick.AddListener(OnConfirmNo);
        }

        public void Update()
        {
            bool showConfirms = controller.SelectedBuilding && needsConfirmation;
            confirmYesButton.gameObject.SetActive(showConfirms);
            confirmNoButton.gameObject.SetActive(showConfirms);

            bool showDestroy = controller.SelectedBuilding && !needsConfirmation;
            destroyButton.gameObject.SetActive(showDestroy);

            if (!controller.SelectedBuilding) return;

            var targetPosition = controller.SelectedBuilding.transform.position;
            var targetScreenPosition = Camera.main.WorldToScreenPoint(targetPosition);

            gameObject.transform.position = targetScreenPosition + new Vector3(offset.x - buttonsContainer.rect.width / 2, offset.y, 0);
        }

        public void OnDestroy()
        {
            needsConfirmation = true;
        }

        public void OnConfirmNo()
        {
            needsConfirmation = false;
        }

        public void OnConfirmYes()
        {
            needsConfirmation = false;

            var building = controller.SelectedBuilding;
            if (building == null) return;

            Tile tile = controller.World[building.Position];
            controller.World.DestroyBuildingsOnTile(tile);
        }
    }
}
