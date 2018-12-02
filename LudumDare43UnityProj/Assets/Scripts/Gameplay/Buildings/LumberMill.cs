using Assets.Scripts.Gameplay.People;
using Assets.Scripts.Gameplay.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    public class LumberMill : WorkableTarget
    {
        [SerializeField] private int woodPerWork;
        [SerializeField] private int checkTreeRadius;

        public override Job job
        {
            get
            {
                return Job.Lumberjack;
            }
        }

        public override void WorkerAssigned()
        {
            base.WorkerAssigned();
        }

        public override void WorkerFreed()
        {
            base.WorkerFreed();
        }

        public override void DoWork(PersonAI aI)
        {
            //aI.MoveToPosition();
            CheckNearby(this.transform.position, checkTreeRadius);
            GameplayController.instance.CurrentResources.Wood += woodPerWork;
        }

        private bool CheckNearby(Vector3 center, float radius)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center, radius);
            List<GameObject> trees = new List<GameObject>();

            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].tag == "Tree")
                {
                    trees.Add(hitColliders[i].gameObject);
                }
                i++;
            }

            Debug.Log(trees.Count);
            return true;
        }
    }
}
