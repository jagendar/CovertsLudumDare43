﻿using System.Linq;
using Assets.Scripts.Gameplay.Buildings;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    [RequireComponent(typeof(BuildModeController))]
    [RequireComponent(typeof(BuildingSelectionController))]
    public class PlayerController : MonoBehaviour
    {
        private BuildModeController buildModeController;
        private BuildingSelectionController buildingSelectionController;

        public ObjectsUnderCursor UnderCursor { get; private set; }

        public void Awake()
        {
            buildModeController = GetComponent<BuildModeController>();
            buildingSelectionController = GetComponent<BuildingSelectionController>();
        }

        public void Update()
        {
            UnderCursor = new ObjectsUnderCursor();

            buildModeController.SubControllerUpdate(this);
            buildingSelectionController.SubControllerUpdate(this);

            if(Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftAlt) && UnderCursor.Tile != null)
            {
                GameplayController.instance.World.DestroyBuildingsOnTile(UnderCursor.Tile);
            }
        }

        public void Build(Building building)
        {
            buildModeController.StartBuilding(building);
        }
    }
}
