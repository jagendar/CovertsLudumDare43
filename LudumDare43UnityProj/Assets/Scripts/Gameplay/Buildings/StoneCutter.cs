using Assets.Scripts.Gameplay.People;
using Assets.Scripts.Gameplay.World;
using Assets.Scripts.Gameplay.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    public class StoneCutter : WorkableTarget
    {
        [SerializeField] private int stonePerWork;
        [SerializeField] private int checkStoneRadius;
        [SerializeField] private string stoneTag;
        [SerializeField] Transform spawnSpot;

        private List<CollectableResource> stoneNearby;
        private CollectableResource nearestStone;
        private Tile nearestTile;
        private World.World world;


        private void Start()
        {
            world = GameplayController.instance.World;
            stoneNearby = CheckNearbyResources(this.transform.position, checkStoneRadius, stoneTag);
            if (stoneNearby.Count == 0)
            {
                this.maxWorkers = 0;
            }
        }

        public override Job job
        {
            get
            {
                return Job.Quarryworker;
            }
        }

        public override void WorkerAssigned(PersonAI aI)
        {
            base.WorkerAssigned(aI);
            aI.transform.position = spawnSpot.position;
            stoneNearby = CheckNearbyResources(this.transform.position, checkStoneRadius, stoneTag);
            if(stoneNearby.Count == 0)
            {
                this.maxWorkers = 0;
                aI.Idle();
                return;
            }
            nearestStone = GetShortestDistance(this.transform.position, stoneNearby, checkStoneRadius);
            nearestTile = CheckNearbyTiles(nearestStone.placedTile, world);
            nearestStone.Worker = aI;
            aI.MoveToPosition(nearestTile);
        }

        public override void WorkerFreed(PersonAI aI)
        {
            nearestStone.Worker = null;
            base.WorkerFreed(aI);  
        }

        public override void DoWork(PersonAI aI)
        {
            if (nearestStone == null)
            {
                aI.ReachedDestination = false;
                stoneNearby = CheckNearbyResources(this.transform.position, checkStoneRadius, stoneTag);
                if (stoneNearby.Count == 0)
                {
                    this.maxWorkers = 0;
                    aI.Idle();
                    return;
                }
                nearestStone = GetShortestDistance(this.transform.position, stoneNearby, checkStoneRadius);
                nearestTile = CheckNearbyTiles(nearestStone.placedTile, world);
                aI.MoveToPosition(nearestTile);
            }
            if (aI.ReachedDestination)
            {
                if (nearestStone == null || nearestStone.Worker != aI)
                {
                    aI.ReachedDestination = false;
                    stoneNearby = CheckNearbyResources(this.transform.position, checkStoneRadius, stoneTag);
                    if (stoneNearby.Count == 0)
                    {
                        this.maxWorkers = 0;
                        aI.Idle();
                        return;
                    }
                    nearestStone = GetShortestDistance(this.transform.position, stoneNearby, checkStoneRadius);
                    nearestTile = CheckNearbyTiles(nearestStone.placedTile, world);
                    aI.MoveToPosition(nearestTile);
                }
                GameplayController.instance.CurrentResources.Stone += stonePerWork;
                if (nearestStone.Anim != null)
                {
                    nearestStone.Anim.Play();
                }
                nearestStone.Amount -= stonePerWork;
            }
        }
    }
}