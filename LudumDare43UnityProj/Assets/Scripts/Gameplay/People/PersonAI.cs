using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Gameplay.Buildings;
using Assets.Scripts.Gameplay.UserInput;
using UnityEngine;
using Assets.Scripts.Gameplay.World;

namespace Assets.Scripts.Gameplay.People
{
    public class PersonAI : MonoBehaviour
    {
        [SerializeField] PersonColorer colorer;
        [SerializeField] LayerMask tileLayermask;
        [SerializeField] Animation wiggleAnim;


        public bool ReachedDestination = false;
        public Tile currentTile { get; private set; }

        public WorkableTarget workTarget;
        private List<Tile> path;

        private IEnumerator moveCoroutine;

        private void Awake()
        {
            UpdateCurrentTile();
            GameplayController.instance.World.AddPerson(this);
            GameplayController.instance.CurrentResources.Population += 1;
            StartCoroutine(DoWork());
        }

        private void Start()
        {
            colorer.SetJobColor(Job.Idle);
            wiggleAnim.Play();
            RunWiggle(false);
        }

        public void UpdateCurrentTile()
        {
            RaycastHit hitInfo;

            // Note: We add an upward vector to the transform position to ensure that they start of the ray is above the tile
            bool hit = Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hitInfo, float.MaxValue, tileLayermask);

            if (hit)
            {
                var tile = hitInfo.transform.gameObject.GetComponent<Tile>();
               Debug.Assert(tile != null, "The collider object should also have a tile component");
                currentTile = tile;
            }
        }

        private void OnDestroy()
        {
            if (GameplayController.instance != null)
            {
                GameplayController.instance.CurrentResources.Population -= 1;
                GameplayController.instance.World.RemovePerson(this);
            }
            if(workTarget != null)
            {
                workTarget.WorkerFreed(this);
            }
        }

        public void Grabbed()
        {
            transform.localScale = new Vector3(2, 2, 2);
            Idle();
            RunWiggle(true);
        }

        internal void Idle()
        {
            if (workTarget != null)
            {
                workTarget.WorkerFreed(this);
            }
            StopMoving();
            ReachedDestination = false;
            colorer.SetJobColor(Job.Idle);
            workTarget = null;
        }

        private void StopMoving()
        {
            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
        }

        internal void DroppedOn(ObjectsUnderCursor underCursor)
        {
            RunWiggle(false);
            transform.localScale = new Vector3(1, 1, 1);

            if (underCursor.Building != null)
            {
                DroppedOnBuilding(underCursor.Building);
            }
            else
            {
                DroppedOnTile(underCursor.Tile);
            }
        }

        internal void DroppedOnBuilding(Building building)
        {
            // TODO: Set location
            var housing = building.GetComponent<Housing>();
            transform.position = housing != null ? housing.SpawnSpot.position : building.transform.position;

            WorkableTarget target = building.GetComponent<WorkableTarget>();
            if (target == null || !target.RoomForWorker) return;

            workTarget = target;
            workTarget.WorkerAssigned(this);
            colorer.SetJobColor(workTarget.job);
        }

        internal void DroppedOnTile(Tile tile)
        {
            if (tile == null)
            {
                Destroy(gameObject);
                return;
            }

            if (tile.IsSacrificable)
            {
                GameplayController.instance.VolcanoController.ResetSpeed();
            }

            if (tile.IsSacrificable || tile.IsDeadly)
            {
                Destroy(gameObject);
                return;
            }

            transform.position = tile.transform.position;
            UpdateCurrentTile();
        }
#if CLICK_DEBUG_MOVEMENT
        public void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, float.MaxValue, tileLayermask);
                
                if (hit)
                {
                    var tile = hitInfo.transform.parent.gameObject.GetComponent<Tile>();
                    Debug.Assert(tile != null, "Tile object's collider should be its immediate child");

                    MoveToPosition(tile);
                }
            }
        }
#endif

        public void MoveToPosition(Tile target)
        {
            if(moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
            moveCoroutine = MoveToPositionCoroutine(target);
            StartCoroutine(moveCoroutine);
        }

        private IEnumerator MoveToPositionCoroutine(Tile target)
        {
            var currentTileP = currentTile.Position;
            var targetP = target.Position;
            List<Tile> path = VolcanoAStar.GetPath(currentTileP, targetP, GameplayController.instance.World);
            if(path == null || path.Count == 0)
            {
                yield break;
            }
            path.RemoveAt(0); //skip current tile

            while(path.Count > 0)
            {
                Tile currentTarget = path[0];
                path.RemoveAt(0);
                yield return StartCoroutine(MoveToTile(currentTarget));
            }

            ReachedDestination = true;
            moveCoroutine = null;
        }

        private IEnumerator MoveToTile(Tile currentTarget)
        {
            const float translationTime = .5f;
            for (float t = 0; t < translationTime; t += Time.deltaTime)
            {
                if (currentTarget.IsSacrificable && t > .5 * translationTime)
                {
                    Destroy(gameObject); //poor bastard walked into lava
                    yield break;
                }
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

        private IEnumerator DoWork()
        {
            while (true)
            {
                if(workTarget != null)
                {
                    workTarget.DoWork(this);
                }
                else
                {
                    colorer.SetJobColor(Job.Idle);
                }
                yield return new WaitForSeconds(1f);
            }
        }

        private void RunWiggle(bool wiggling)
        {
            wiggleAnim["wiggle"].speed = wiggling ? 1 : 0;
            if(!wiggling)
            {
                wiggleAnim["wiggle"].time = 0;
            }
        }
    }
}
