using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class Tile : MonoBehaviour
    {

        [SerializeField]
        private bool buildable;
        
        public bool IsBuildable
        {
            get { return this.buildable; }
        }

        public float Height
        {
            get { return this.transform.position.y; }
            set { this.transform.position = new Vector3(this.transform.position.x, value, this.transform.position.z); }
        }
    }
}
