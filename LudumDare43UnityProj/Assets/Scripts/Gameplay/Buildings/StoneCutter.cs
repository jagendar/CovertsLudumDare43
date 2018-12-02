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

        private List<CollectableResource> stoneNearby;
        private CollectableResource nearestStone;
        private Tile nearestTile;
        private World.World world;


        private void Start()
        {
            world = GameplayController.instance.World;
            stoneNearby = CheckNearbyStone(this.transform.position, checkStoneRadius);
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
            nearestStone = GetShortestDistance(this.transform.position, stoneNearby);
            nearestTile = CheckNearbyTiles(nearestStone.placedTile);
            nearestStone.Worker = aI;
            aI.MoveToPosition(nearestTile);
        }

        public override void WorkerFreed(PersonAI aI)
        {
            base.WorkerFreed(aI);
        }

        public override void DoWork(PersonAI aI)
        {
            if (nearestStone == null)
            {
                aI.ReachedDestination = false;
                stoneNearby = CheckNearbyStone(this.transform.position, checkStoneRadius);
                if (stoneNearby.Count == 0)
                {
                    this.maxWorkers = 0;
                    aI.Idle();
                    return;
                }
                nearestStone = GetShortestDistance(this.transform.position, stoneNearby);
                nearestTile = CheckNearbyTiles(nearestStone.placedTile);
                aI.MoveToPosition(nearestTile);
            }
            if (aI.ReachedDestination)
            {
                if (nearestStone == null || nearestStone.Worker != aI)
                {
                    aI.ReachedDestination = false;
                    stoneNearby = CheckNearbyStone(this.transform.position, checkStoneRadius);
                    if (stoneNearby.Count == 0)
                    {
                        this.maxWorkers = 0;
                        aI.Idle();
                        return;
                    }
                    nearestStone = GetShortestDistance(this.transform.position, stoneNearby);
                    nearestTile = CheckNearbyTiles(nearestStone.placedTile);
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

        private List<CollectableResource> CheckNearbyStone(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            List<CollectableResource> trees = new List<CollectableResource>();

            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "Rock" && hitColliders[i].gameObject.GetComponent<CollectableResource>().Worker == null)
                {
                    trees.Add(hitColliders[i].gameObject.GetComponent<CollectableResource>());
                }
                i++;
            }
            return trees;
        }

        private Tile CheckNearbyTiles(Tile tile)
        {
            Vector2Int pos = tile.Position;
            for (int i = pos.x - 1; i <= pos.x + 1; i++)
            {
                for (int j = pos.y - 1; j <= pos.y + 1; j++)
                {
                    if (world[i, j].IsBuildable)
                    {
                        return world[i, j];
                    }
                }
            }
            return null;
        }

        private CollectableResource GetShortestDistance(Vector3 position, List<CollectableResource> objects)
        {
            CollectableResource tree = objects[0];
            float dist = checkStoneRadius;
            for (int i = 0; i < objects.Count; i++)
            {
                float tempDist = Vector3.Distance(position, objects[i].transform.position);
                if (tempDist < dist)
                {
                    dist = tempDist;
                    tree = objects[i];
                }
            }

            return tree;
        }
    }
}