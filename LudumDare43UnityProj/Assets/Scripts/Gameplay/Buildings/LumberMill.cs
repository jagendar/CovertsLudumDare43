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

        private List<CollectableResource> treesNearby;
        private CollectableResource nearestTree;
        private Tile nearestTile;
        private World.World world;

        
        private void Start()
        {
            world = GameplayController.instance.World;
            treesNearby = CheckNearbyTrees(this.transform.position, checkTreeRadius);
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
            nearestTree = GetShortestDistance(this.transform.position, treesNearby);
            nearestTile = CheckNearbyTiles(nearestTree.placedTile);
            aI.MoveToPosition(nearestTile);
        }

        public override void WorkerFreed(PersonAI aI)
        {
            base.WorkerFreed(aI);
        }

        public override void DoWork(PersonAI aI)
        {
            if(nearestTree == null)
            {
                aI.ReachedDestination = false;
                treesNearby = CheckNearbyTrees(this.transform.position, checkTreeRadius);
                if(treesNearby.Count == 0)
                {
                    this.maxWorkers = 0;
                    aI.Grabbed();
                    return;
                }
                nearestTree = GetShortestDistance(this.transform.position, treesNearby);
                nearestTile = CheckNearbyTiles(nearestTree.placedTile);
                aI.MoveToPosition(nearestTile);
            }
            if(aI.ReachedDestination)
            {
                GameplayController.instance.CurrentResources.Wood += woodPerWork;
                if (nearestTree.Anim != null)
                {
                    nearestTree.Anim.Play();
                }
                nearestTree.Amount-= woodPerWork;
            }
        }

        private List<CollectableResource> CheckNearbyTrees(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            List<CollectableResource> trees = new List<CollectableResource>();

            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "Tree")
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
            for(int i = pos.x - 1; i <= pos.x + 1; i++)
            {
                for(int j = pos.y - 1; j <= pos.y + 1; j++)
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
            float dist = checkTreeRadius;
            for(int i = 0; i < objects.Count; i++)
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
