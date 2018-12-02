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

        private void Start()
        {
            treesNearby = CheckNearby(this.transform.position, checkTreeRadius);
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
            //aI.MoveToPosition(nearestTree.placedTile);
        }

        public override void WorkerFreed(PersonAI aI)
        {
            base.WorkerFreed(aI);
        }

        public override void DoWork(PersonAI aI)
        {
            GameplayController.instance.CurrentResources.Wood += woodPerWork;
        }

        private List<CollectableResource> CheckNearby(Vector3 center, float radius)
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
