using System.Collections.Generic;
using Assets.Scripts.Gameplay.Buildings;
using Assets.Scripts.Gameplay.World;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        public static GameplayController instance { get; private set; }

        [SerializeField] private PopulateWorld worldPopulator = null;
        [SerializeField] private Volcano volcano;

        public World.World World { get; private set; }

        public Resources.ResourceCollection CurrentResources;

        public int maxPopulation { get; set; }
        public Volcano VolcanoController { get { return volcano; } }

        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            if(instance == this)
            {
                instance = null;
            }
        }

        public void Start()
        {
            World = worldPopulator.World;
           // volcano.BYFIREBEPURGED(World);
        }
    }
}
