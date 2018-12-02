using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class LavaShot : MonoBehaviour
    {
        [SerializeField] Material lavaMat;

        internal void Initialize(Tile tile)
        {
            StartCoroutine(MoveToTile(tile));
        }

        private IEnumerator MoveToTile(Tile currentTarget)
        {
            Vector3 pos = transform.position;
            const float translationTime = 2.5f;
            for (float t = 0; t < translationTime; t += Time.deltaTime)
            {
                SlideLerp(pos, currentTarget.transform.position, t / translationTime);
                yield return null;
            }
            SlideLerp(pos, currentTarget.transform.position, 1);
        }

        private void SlideLerp(Vector3 startPosition, Vector3 targetPosition, float v)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, v);
        }
    }
}
