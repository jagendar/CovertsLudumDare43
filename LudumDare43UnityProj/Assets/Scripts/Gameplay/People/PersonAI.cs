﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Gameplay.World;

namespace Assets.Scripts.Gameplay.People
{
    public class PersonAI : MonoBehaviour
    {
        [SerializeField] PersonColorer colorer;
        [SerializeField] LayerMask tileLayermask;

        public Tile currentTile { get; private set; }

        private List<Tile> path;

        private void Awake()
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(transform.position, Vector3.down, out hitInfo, float.MaxValue, tileLayermask);

            if (hit)
            {
                var tile = hitInfo.transform.parent.gameObject.GetComponent<Tile>();
                Debug.Assert(tile != null, "Tile object's collider should be its immediate child");
                currentTile = tile;
            }
        }

        [SerializeField] Tile testTile;
        [ContextMenu("Move Test")]
        private void MoveTest()
        {
            MoveToPosition(testTile);
        }

        public void MoveToPosition(Tile target)
        {
            StartCoroutine(MoveToPositionCoroutine(target));
        }

        private IEnumerator MoveToPositionCoroutine(Tile target)
        {
            List<Tile> path = VolcanoAStar.GetPath(currentTile.Position, target.Position, GameplayController.instance.World);
            if(path == null)
            {
                yield break;
            }

            while(path.Count > 0)
            {
                Tile currentTarget = path[0];
                path.RemoveAt(0);
                yield return StartCoroutine(MoveToTile(currentTarget));
            }
        }

        private IEnumerator MoveToTile(Tile currentTarget)
        {
            const float translationTime = .5f;
            for (float t = 0; t < translationTime; t += Time.deltaTime)
            {
                SlideLerp(currentTile, currentTarget, t / translationTime);
                yield return null;
            }
            SlideLerp(currentTile, currentTarget, 1);
            currentTile = currentTarget;
        }

        private void SlideLerp(Tile currentTile, Tile currentTarget, float v)
        {
            transform.position = Vector3.Lerp(currentTile.transform.position, currentTarget.transform.position, v);
        }
    }
}