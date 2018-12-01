using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private PopulateWorld worldPopulator = null;

        public World.World World { get; private set; }

        public Resources.ResourceCollection currentResources;

        public void Start()
        {
            World = worldPopulator.World;
        }
    }
}
