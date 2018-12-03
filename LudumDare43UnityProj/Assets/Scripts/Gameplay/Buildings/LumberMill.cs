using Assets.Scripts.Gameplay.People;
using Assets.Scripts.Gameplay.World;
using Assets.Scripts.Gameplay.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    public class LumberMill : WorkableTarget
    {
        [SerializeField] private int woodPerWork;
        [SerializeField] private int checkTreeRadius;
        [SerializeField] private string treeTag;
        [SerializeField] private Transform spawnSpot;
        [SerializeField] private List<GameObject> lumber;

        private List<CollectableResource> treesNearby;
        private CollectableResource nearestTree;
        private Tile nearestTile;
        private World.World world;

        public override int ResourceRadius
        {
            get { return checkTreeRadius; }
        }

        public override string ResourceTag
        {
            get { return treeTag; }
        }

        private void Start()
        {
            world = GameplayController.instance.World;
            treesNearby = CheckNearbyResources(this.transform.position);
            if (treesNearby.Count == 0)
            {
                SetBuildingEmpty(lumber);
                this.maxWorkers = 0;
            }
        }

        public override Job job
        {
            get
            {
                return Job.Lumberjack;
            }
        }

        public override void WorkerAssigned(PersonAI aI)
        {
            base.WorkerAssigned(aI);
            aI.transform.position = spawnSpot.position;
            aI.UpdateCurrentTile();
            treesNearby = CheckNearbyResources(this.transform.position);
            if(treesNearby.Count == 0)
            {
                SetBuildingEmpty(lumber);
                this.maxWorkers = 0;
                aI.Idle();
                return;
            }
            nearestTree = GetShortestDistance(this.transform.position, treesNearby, checkTreeRadius);
            nearestTile = CheckNearbyTiles(nearestTree.placedTile, world);
            nearestTree.Worker = aI;
            aI.MoveToPosition(nearestTile);
        }

        public override void WorkerFreed(PersonAI aI)
        {
            nearestTree.Worker = null;
            base.WorkerFreed(aI);
        }

        public override void DoWork(PersonAI aI)
        {
            if(nearestTree == null)
            {
                aI.ReachedDestination = false;
                treesNearby = CheckNearbyResources(this.transform.position);
                if(treesNearby.Count == 0)
                {
                    SetBuildingEmpty(lumber);
                    this.maxWorkers = 0;
                    aI.Idle();
                    return;
                }
                nearestTree = GetShortestDistance(this.transform.position, treesNearby, checkTreeRadius);
                nearestTile = CheckNearbyTiles(nearestTree.placedTile, world);
                aI.MoveToPosition(nearestTile);
            }
            if (aI.ReachedDestination)
            {
                if (nearestTree == null || nearestTree.Worker != aI)
                {
                    aI.ReachedDestination = false;
                    treesNearby = CheckNearbyResources(this.transform.position);
                    if (treesNearby.Count == 0)
                    {
                        SetBuildingEmpty(lumber);
                        this.maxWorkers = 0;
                        aI.Idle();
                        return;
                    }
                    nearestTree = GetShortestDistance(this.transform.position, treesNearby, checkTreeRadius);
                    nearestTile = CheckNearbyTiles(nearestTree.placedTile, world);
                    aI.MoveToPosition(nearestTile);
                }
                GameplayController.instance.CurrentResources.Wood += woodPerWork;
                if (nearestTree.Anim != null)
                {
                    nearestTree.Anim.Play();
                }
                nearestTree.Amount-= woodPerWork;
            }
        }
    }
}
