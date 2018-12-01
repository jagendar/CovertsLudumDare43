using Assets.Scripts.Gameplay.Buildings;
using Assets.Scripts.Gameplay.World;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Assets.Scripts.Gameplay.UserInput
{
    class BuildModeController : MonoBehaviour
    {
        private bool isBuilding;

        private Building template;

        [SerializeField] private LayerMask layerMask;

        private GameObject hologram;

        public void StartBuilding(Building buildingTemplate)
        {
            isBuilding = true;
            template = buildingTemplate;

            hologram = Instantiate(template.ConstructionHologram);
            hologram.SetActive(false);
        }

        public void CancelBuilding()
        {
            isBuilding = false;
            template = null;
            if (hologram != null)
            {
                Destroy(hologram);
                hologram = null;
            }
        }

        public void Update()
        {
            if (!isBuilding) return;

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, float.MaxValue, layerMask);

            if (hit)
            {
                var tile = hitInfo.transform.parent.gameObject.GetComponent<Tile>();
                Debug.Assert(tile != null, "Tile object's collider should be its immediate child");

                //Debug.Log(tile.Position);
                hologram.SetActive(true);

                var holoPos = tile.transform.position;
                //holoPos.x -= 0.5f;// + template.Pivot.x;
                holoPos.y += 0;
                //holoPos.z -= 0.5f;// + template.Pivot.y;
                hologram.transform.position = holoPos;

                Debug.Log(hologram.transform.position);
            }
            else
            {
                Debug.Log("No hit");
                hologram.SetActive(false);
            }
        }
    }
}
