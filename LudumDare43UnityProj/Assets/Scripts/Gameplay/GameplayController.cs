﻿using System.Collections.Generic;
using Assets.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private PopulateWorld worldPopulator = null;

        public World.World World { get; private set; }

        public List<Building> Buildings { get; private set; }

        public Resources.ResourceCollection currentResources;

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
