using Assets.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    [RequireComponent(typeof(BuildModeController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private GameplayController gameplayController;

        private BuildModeController buildMode;

        public void Start()
        {
            buildMode = GetComponent<BuildModeController>();
        }

        public void Build(Building building)
        {
            buildMode.StartBuilding(building);
        }
    }
}
