using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class LavaShot : MonoBehaviour
    {
        private Tile target;

        internal void Initialize(Tile tile)
        {
            target = tile;    
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, .1f);
        }
    }
}
