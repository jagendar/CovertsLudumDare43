using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Gameplay.Buildings;
using Assets.Scripts.Gameplay.People;
using Assets.Scripts.Gameplay.World;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class ObjectsUnderCursor
    {
        public Building Building { get; set; }
        public Tile Tile { get; set; }
        public PersonAI Person { get; set; }

        public ObjectsUnderCursor()
        {
            var objects = GetObjectsUnderCursor();

            Building = objects.Select(o => o.GetComponent<Building>()).FirstOrDefault();
            Tile = objects.Select(o => o.GetComponentInParent<Tile>()).FirstOrDefault();
            Person = objects.Select(o => o.GetComponent<PersonAI>()).FirstOrDefault();
        }

        public IEnumerable<GameObject> GetObjectsUnderCursor()
        {
            var mask = LayerMask.GetMask("Tiles", "Buildings", "People");

            var hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), float.MaxValue, mask);

            //var objs = string.Join(", ", hits.Select(h => h.transform.gameObject.name).ToArray());
            //Debug.Log(objs);
            return hits.Select(h => h.transform.gameObject);
        }
    }
}
