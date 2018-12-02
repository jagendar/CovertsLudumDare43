using Assets.Scripts.Gameplay.Resources;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class CheatController : MonoBehaviour
    {
        [SerializeField] private bool cheatsEnabled = false;
        [SerializeField] private GameplayController gameplayController;

        public void Update()
        {
            if (!cheatsEnabled) return;

            if (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.R))
            {
                gameplayController.CurrentResources = new ResourceCollection
                {
                    Stone = 9999,
                    Food = 9999,
                    Wood = 9999,
                    Population = gameplayController.CurrentResources.Population
                };
            }
        }
    }
}
