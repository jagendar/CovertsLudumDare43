using Assets.Scripts.Gameplay.People;
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
    }
}