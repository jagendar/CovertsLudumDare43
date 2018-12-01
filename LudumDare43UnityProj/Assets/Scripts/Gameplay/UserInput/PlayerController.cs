using Assets.Scripts.Gameplay.Buildings;
using Assets.Scripts.Gameplay.UserInput;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Player
{
    [RequireComponent(typeof(BuildModeController))]
    class PlayerController : MonoBehaviour
    {
        private BuildModeController buildMode;

        [SerializeField]
        private Building testBuilding;

        public void Start()
        {
            buildMode = GetComponent<BuildModeController>();
            buildMode.StartBuilding(testBuilding);
        }
    }
}
