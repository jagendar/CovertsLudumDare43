using Assets.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    [RequireComponent(typeof(BuildModeController))]
    public class PlayerController : MonoBehaviour
    {
        private BuildModeController buildMode;

        [SerializeField]
        private Building testBuilding;

        public World.World World { get; set; }

        public void Start()
        {
            buildMode = GetComponent<BuildModeController>();
        }

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.B))
            {
                Build(testBuilding);
            }
        }

        public void Build(Building building)
        {
            buildMode.StartBuilding(building);
        }
    }
}
