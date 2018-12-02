using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class LavaShot : MonoBehaviour
    {
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

            CauseDestruction(currentTarget);
        }

        private void CauseDestruction(Tile target)
        {
            World world = GameplayController.instance.World;
            List<Tile> tilesToDestroy = new List<Tile>(5);
            tilesToDestroy.Add(target);
            if(target.Position.x > 0)
            {
                tilesToDestroy.Add(world[target.Position.x - 1, target.Position.y]);
            }
            if (target.Position.y > 0)
            {
                tilesToDestroy.Add(world[target.Position.x, target.Position.y - 1]);
            }

            if (target.Position.x < world.Width - 1)
            {
                tilesToDestroy.Add(world[target.Position.x + 1, target.Position.y]);
            }
            if (target.Position.y < world.Height - 1)
            {
                tilesToDestroy.Add(world[target.Position.x, target.Position.y + 1]);
            }

            LavaDestruction(tilesToDestroy, world);
        }

        private void LavaDestruction(List<Tile> tilesToDestroy, World world)
        {
            for(int i = 0; i < tilesToDestroy.Count; ++i)
            {
                Tile t = world[tilesToDestroy[i].Position];
                t.BecomeLava();

                world.DestroyBuildingsOnTile(tilesToDestroy[i]);
                world.DestroyResourcesOnTile(tilesToDestroy[i]);
                world.DestroyPeopleOnTile(tilesToDestroy[i]);
            }

            Destroy(gameObject);
        }

        private void SlideLerp(Vector3 startPosition, Vector3 targetPosition, float v)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, v);
        }
    }
}
