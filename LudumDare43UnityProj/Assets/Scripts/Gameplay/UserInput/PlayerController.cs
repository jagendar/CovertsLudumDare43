using System.Linq;
using Assets.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    [RequireComponent(typeof(BuildModeController))]
    [RequireComponent(typeof(BuildingSelectionController))]
    [RequireComponent(typeof(PersonDragController))]
    public class PlayerController : MonoBehaviour
    {
        private BuildModeController buildModeController;
        private BuildingSelectionController buildingSelectionController;
        private PersonDragController personDragController;

        public ObjectsUnderCursor UnderCursor { get; private set; }

        public void Awake()
        {
            buildModeController = GetComponent<BuildModeController>();
            buildingSelectionController = GetComponent<BuildingSelectionController>();
            personDragController = GetComponent<PersonDragController>();
        }

        public void Update()
        {
            UnderCursor = new ObjectsUnderCursor();

            buildModeController.SubControllerUpdate(this);
            buildingSelectionController.SubControllerUpdate(this);
            personDragController.SubComponentUpdate(this);
        }

        public void Build(Building building)
        {
            buildModeController.StartBuilding(building);
        }
    }
}
