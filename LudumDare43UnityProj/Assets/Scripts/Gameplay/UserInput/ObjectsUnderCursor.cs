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
            Debug.Log(string.Join(", ", objects.Select(obj => obj.name).ToArray()));

            Building = objects.Select(o => o.GetComponent<Building>()).FirstOrDefault(c => c != null);
            Tile = objects.Select(o => o.GetComponent<Tile>()).FirstOrDefault(c => c != null);
            Person = objects.Select(o => o.GetComponent<PersonAI>()).FirstOrDefault(c => c != null);
        }

        private IEnumerable<GameObject> GetObjectsUnderCursor()
        {
            var mask = LayerMask.GetMask("Tiles", "Buildings", "People");

            var hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), float.MaxValue, mask);

            return hits.Select(h => h.transform.gameObject);
        }
    }
}
