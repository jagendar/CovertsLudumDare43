using Assets.Scripts.Gameplay.People;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public abstract class IWorkableTarget : MonoBehaviour
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

        public void WorkerAssigned()
        {
            currentWorkers++;
        }

        public void WorkerFreed()
        {
            currentWorkers--;
        }

        public abstract void DoWork(PersonAI aI);
        public abstract Job job { get; }
    }
}