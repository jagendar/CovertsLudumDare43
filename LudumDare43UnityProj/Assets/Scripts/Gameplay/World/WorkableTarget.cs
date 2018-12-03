using Assets.Scripts.Gameplay.People;
using Assets.Scripts.Gameplay.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public abstract class WorkableTarget : MonoBehaviour
    {
        [SerializeField] protected int maxWorkers = 1;

        private int currentWorkers = 0;

        public bool RoomForWorker
        {
            get
            {
                return currentWorkers < maxWorkers;
            }
        }

        public virtual void WorkerAssigned(PersonAI aI)
        {
            currentWorkers++;
        }

        public virtual void WorkerFreed(PersonAI aI)
        {
            currentWorkers--;
        }

        public abstract void DoWork(PersonAI aI);

        public abstract Job job { get; }

        public abstract int ResourceRadius { get; }

        public abstract string ResourceTag { get; }


        protected Tile CheckNearbyTiles(Tile tile, World world)
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

        public List<CollectableResource> CheckNearbyResources(Vector3 center)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, ResourceRadius);
            List<CollectableResource> resources = new List<CollectableResource>();

            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == ResourceTag && hitColliders[i].gameObject.GetComponent<CollectableResource>().Worker == null)
                {
                    resources.Add(hitColliders[i].gameObject.GetComponent<CollectableResource>());
                }
                i++;
            }
            return resources;
        }

        protected CollectableResource GetShortestDistance(Vector3 position, List<CollectableResource> objects, int radius)
        {
            CollectableResource tree = objects[0];
            float dist = radius;
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

        protected void SetBuildingEmpty(List<GameObject> resources)
        {
            for(int i=0; i<resources.Count; i++)
            {
                Destroy(resources[i].gameObject);
            }
        }
    }
}