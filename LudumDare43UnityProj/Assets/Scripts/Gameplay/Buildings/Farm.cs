using Assets.Scripts.Gameplay.People;
using Assets.Scripts.Gameplay.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Buildings
{
    public class Farm : IWorkableTarget
    {
        [SerializeField] private int foodPerWork = 7;


        public override Job job
        {
            get
            {
                return Job.Farmer;
            }
        }

        public override void DoWork(PersonAI aI)
        {
            GameplayController.instance.CurrentResources.Food += foodPerWork;
        }
    }
}