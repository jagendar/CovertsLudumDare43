using System.Collections.Generic;
using Assets.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        public static GameplayController instance { get; private set; }

        [SerializeField] private PopulateWorld worldPopulator = null;

        public World.World World { get; private set; }

        public List<Building> Buildings { get; private set; }

        public Resources.ResourceCollection currentResources;

        public Resources.ResourceCollection currentResources { get; set; }

        public int maxPopulation { get; set; }

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

        public GameplayController()
        {
            Buildings = new List<Building>();

        }

        public void Start()
        {
            World = worldPopulator.World;
        }
    }
}
