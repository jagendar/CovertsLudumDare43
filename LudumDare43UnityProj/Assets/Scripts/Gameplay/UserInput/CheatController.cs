using Assets.Scripts.Gameplay.Resources;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class CheatController : MonoBehaviour
    {
        private bool cheatsEnabled = false;
        [SerializeField] private GameplayController gameplayController;
        [SerializeField] private GameObject cheatFace;

        public void Update()
        {
            if (!cheatsEnabled)
            {
                if((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    && Input.GetKeyDown(KeyCode.F9))
                {
                    cheatsEnabled = true;
                    if(cheatFace != null)
                    {
                        cheatFace.SetActive(true);
                    }
                }

                return;
            }

            if (Input.GetKeyDown(KeyCode.F10))
            {
                gameplayController.CurrentResources = new ResourceCollection
                {
                    Stone = 9999,
                    Food = 9999,
                    Wood = 9999,
                    Population = gameplayController.CurrentResources.Population
                };
            }

            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftAlt) && gameplayController.PlayerController.UnderCursor.Tile != null)
            {
                gameplayController.VolcanoController.TOOSOONEXECUTUS(gameplayController.PlayerController.UnderCursor.Tile);
            }
        }
    }
}
