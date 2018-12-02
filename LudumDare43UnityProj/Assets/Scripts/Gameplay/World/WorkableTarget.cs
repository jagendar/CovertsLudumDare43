using Assets.Scripts.Gameplay.People;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public abstract class WorkableTarget : MonoBehaviour
    {
        [SerializeField] private int maxWorkers = 1;

        private int currentWorkers = 0;

        public bool RoomForWorker
        {
            get
            {
                return currentWorkers < maxWorkers;
            }
        }

        public virtual void WorkerAssigned()
        {
            currentWorkers++;
        }

        public virtual void WorkerFreed()
        {
            currentWorkers--;
        }

        public abstract void DoWork(PersonAI aI);
        public abstract Job job { get; }
    }
}