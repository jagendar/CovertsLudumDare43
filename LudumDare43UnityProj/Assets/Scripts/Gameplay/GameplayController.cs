using System.Collections.Generic;
using Assets.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private PopulateWorld worldPopulator = null;

        public World.World World { get; private set; }

        public Resources.ResourceCollection currentResources { get; set; }

        public int maxPopulation { get; set; }

        public void Start()
        {
            World = worldPopulator.World;
        }
    }
}
