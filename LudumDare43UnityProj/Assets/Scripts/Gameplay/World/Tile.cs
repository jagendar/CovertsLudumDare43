using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class Tile : ITile
    {
        [SerializeField]
        private bool buildable;

        [SerializeField]
        private int height;

        public bool IsBuildable
        {
            get { return this.buildable; }
        }

        public int Height
        {
            get { return this.height; }
        }
    }
}
