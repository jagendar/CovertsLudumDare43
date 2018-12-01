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

        private Hologram hologram;

        public bool IsBuilding { get; private set; }

        public void StartBuilding(Building buildingTemplate)
        {
            if (IsBuilding)
            {
                CancelBuilding();
            }

            IsBuilding = true;
            template = buildingTemplate;

            hologram = Instantiate(template.ConstructionHologram);
        }

        public void CancelBuilding()
        {
            IsBuilding = false;
            template = null;
            if (hologram != null)
            {
                Destroy(hologram.gameObject);
                hologram = null;
            }
        }

        public void Update()
        {
            if (!IsBuilding) return;

            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
            {
                CancelBuilding();
                return;
            }

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, float.MaxValue, layerMask);

            Tile tile = null;
            if (hit)
            {
                tile = hitInfo.transform.parent.gameObject.GetComponent<Tile>();
                Debug.Assert(tile != null, "Tile object's collider should be its immediate child");
            }

            if (tile != null)
            {
                hologram.Enabled = true;

                // TODO: Account for the pivot of the building... Right now this happens to work cause the it positions based on teh center
                var holoPos = tile.transform.position;
                hologram.transform.position = holoPos;

                // TODO: Should this be offset to the corner of the building?
                // TODO: This doesn't check the proper tiles
                hologram.IsValid = Util.CanBuildAt(gameplayController.World, tile.Position, template);
            }
            else
            {
                hologram.Enabled = false;
                hologram.IsValid = false;
            }
        }
    }
}
