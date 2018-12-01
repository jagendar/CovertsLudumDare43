using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    public class Hologram : MonoBehaviour
    {
        [SerializeField] private Renderer[] renderers;

        [SerializeField] private Material validMaterial;

        [SerializeField] private Material invalidMaterial;

        public bool IsValid
        {
            set
            {
                foreach (var r in renderers)
                {
                    r.material = value ? validMaterial : invalidMaterial;
                }
            }
        }

        public bool Enabled
        {
            set
            {
                foreach (var r in renderers)
                {
                    r.enabled = value;
                }
            }
        }

        public void Start()
        {
            IsValid = true;
            Enabled = true;
        }
    }
}
