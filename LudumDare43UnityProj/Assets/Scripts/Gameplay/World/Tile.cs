﻿using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private Renderer[] renderers;

        [SerializeField]
        private Material lavaMaterial;

        [SerializeField]
        private bool buildable;

        public Vector2Int Position { get; set; }

        private bool temporaryBuildability = true;

        /// <summary>
        /// If true, dropping a person on this tile will sacrifice him/her.
        /// </summary>
        [SerializeField]
        private bool sacrifiable = false;

        [SerializeField]
        private bool deadly;

        public bool IsBuildable
        {
            get { return buildable && temporaryBuildability; }
            set { temporaryBuildability = value; }
        }

        public float Height
        {
            get { return this.transform.position.y; }
            set { this.transform.position = new Vector3(this.transform.position.x, value, this.transform.position.z); }
        }

        public bool IsWalkable
        {
            //TODO: maybe this should be separate... but for now, just piggybacking w/ a separate property for ease of later refactoring
            get { return IsBuildable; }
        }

        public bool IsSacrificable
        {
            get { return sacrifiable;  }
        }

        public bool IsDeadly
        {
            get { return deadly; }
        }

        internal void BecomeLava()
        {
            buildable = false; //it's lava now ya shits
            foreach(var renderer in renderers)
            {
                renderer.material = lavaMaterial;
            }
            sacrifiable = true;
        }
    }
}
