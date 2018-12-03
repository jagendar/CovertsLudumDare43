using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gameplay.Buildings;
using Assets.Scripts.Gameplay.World;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class BuildingSelectionController : MonoBehaviour
    {
        private IEnumerable<WorldObjectHightlight> currentlyHighlighted = new List<WorldObjectHightlight>();

        public void SubControllerUpdate(PlayerController playerController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var controller = GameplayController.instance;

                SetBuildingHightlight(false);
                controller.SelectedBuilding = playerController.UnderCursor.Building;
                UpdateHighlightedResources(controller.SelectedBuilding);
                SetBuildingHightlight(true);
            }
        }

        private void SetBuildingHightlight(bool newValue)
        {
            var building = GameplayController.instance.SelectedBuilding;
            if (building != null)
            {
                var highlight = building.GetComponent<WorldObjectHightlight>();
                if (highlight != null) highlight.IsHighlighted = newValue;
            }
        }

        private void UpdateHighlightedResources(Building building)
        {
            SetHighlights(currentlyHighlighted, false);

            if (building == null) return;

            var workableTarget = building.GetComponent<WorkableTarget>();
            if (workableTarget == null) return;

            currentlyHighlighted = workableTarget.CheckNearbyResources(building.transform.position, true)
                .Where(r => r != null)
                .Select(r => r.GetComponent<WorldObjectHightlight>())
                .Where(rh => rh != null);

            SetHighlights(currentlyHighlighted, true);
        }

        private void SetHighlights(IEnumerable<WorldObjectHightlight> highlights, bool newValue)
        {
            foreach (var resourceHighlight in highlights.Where(h => h != null))
            {
                resourceHighlight.IsHighlighted = newValue;
            }
        }
    }
}
