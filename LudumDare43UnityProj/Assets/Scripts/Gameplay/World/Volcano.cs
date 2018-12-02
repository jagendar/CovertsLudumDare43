using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.World
{
    public class Volcano : MonoBehaviour {
        [SerializeField] private LavaShot eruptingLavaPrefab;
        [SerializeField] private float initialTimeBetweenEruptions = 10;
        [SerializeField] private float eruptionSpeedAccelerationPercentage = .1f;

        private World world;
        private float timeBetweenEruptions;

        public void ResetSpeed()
        {
            timeBetweenEruptions = initialTimeBetweenEruptions;
        }

        public void BYFIREBEPURGED(World world)
        {
            timeBetweenEruptions = initialTimeBetweenEruptions;
            this.world = world;
            int x = world.Width / 2;
            int z = world.Height / 2;

            transform.position = world[x, z].transform.position;
            StartCoroutine(DIEINSECT());
        }

        IEnumerator DIEINSECT()
        {
            while (true)
            {
                LavaShot shot = Instantiate(eruptingLavaPrefab, transform.position, transform.rotation);
                shot.Initialize(GetTargetTile());
                yield return new WaitForSeconds(timeBetweenEruptions);
                timeBetweenEruptions -= eruptionSpeedAccelerationPercentage;
            }
        }

        private Tile GetTargetTile()
        {
            int x = Random.Range(0, world.Width);
            int y = Random.Range(0, world.Height);

            return world[x, y];
        }
    }
}
