using System;
using Assets.Scripts.Gameplay.Buildings;
using Assets.Scripts.Gameplay.World;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class BuildModeController : MonoBehaviour
    {
        private Building template;

        [SerializeField] private LayerMask layerMask;
        [SerializeField] private GameplayController gameplayController = null;

        private bool ignoreClicks;
        private Hologram hologram;

        private bool isShiftBuilding;

        public bool IsBuilding { get; private set; }

        public void StartBuilding(Building buildingTemplate)
        {
            if (IsBuilding)
            {
                EndBuilding();
            }

            ignoreClicks = true;
            IsBuilding = true;
            template = buildingTemplate;

            hologram = Instantiate(template.ConstructionHologram);
        }

        public void EndBuilding()
        {
            IsBuilding = false;
            isShiftBuilding = false;
            template = null;
            if (hologram != null)
            {
                Destroy(hologram.gameObject);
                hologram = null;
            }
        }

        public void SubControllerUpdate(PlayerController playerController)
        {
            if (!IsBuilding) return;

            if (CheckShouldCancel())
            {
                EndBuilding();
                return;
            }

            Tile tile = playerController.UnderCursor.Tile;
            bool isValidPosition = tile != null && Util.CanBuildAt(gameplayController.World, tile.Position, template);

            UpdateHologramState(tile, isValidPosition);

            if (isValidPosition && Input.GetMouseButtonUp(0) && !ignoreClicks)
            {
                gameplayController.World.CreateNewBuilding(template, tile.Position);

                if (Input.GetKey(KeyCode.LeftShift) && GameplayController.instance.CurrentResources >= template.Cost)
                {
                    isShiftBuilding = true;
                }
                else
                {
                    EndBuilding();
                }
            }

            ignoreClicks = false;
        }

        private bool CheckShouldCancel()
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                return true;
            }

            return isShiftBuilding && !Input.GetKey(KeyCode.LeftShift);
        }

        private void UpdateHologramState(Tile tile, bool isValidPosition)
        {
            if (tile != null)
            {
                hologram.Enabled = true;

                var holoPos = tile.transform.position;
                hologram.transform.position = holoPos;

                hologram.IsValid = isValidPosition;
            }
            else
            {
                hologram.Enabled = false;
                hologram.IsValid = false;
            }
        }
    }
}
