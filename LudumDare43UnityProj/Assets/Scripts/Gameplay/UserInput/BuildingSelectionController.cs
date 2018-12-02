using Assets.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class BuildingSelectionController : MonoBehaviour
    {

        public void SubControllerUpdate(PlayerController playerController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameplayController.instance.SelectedBuilding = playerController.UnderCursor.Building;
            }
        }
    }
}
