using Assets.Scripts.Gameplay.People;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDrag : MonoBehaviour {
 
    private Camera cam;
    private GameObject UnderCursor;
    private bool dragging;

    [SerializeField] private LayerMask droppableLayers;

	void Start ()
    {
        cam = Camera.main;
        dragging = false;	
	}

    private void Update()
    {
        if(dragging)
        {
            RaycastFromCam();
        }
    }

    private void RaycastFromCam()
    {
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 99999, droppableLayers))
        {
            float hitX = hit.point.x;
            float hitZ = hit.point.z;
            float hitY = hit.point.y;

            UnderCursor = hit.transform.gameObject;

            Vector3 hitPos = new Vector3(hitX, hitY, hitZ);

            this.transform.position = hitPos;
        }
    }

    private void OnMouseDown()
    {
        dragging = true;
        GetComponent<PersonAI>().Grabbed();
    }

    private void OnMouseUp()
    {
        if (UnderCursor.name == "Dirt" || UnderCursor.name == "Lava")
        {
            Destroy(this.gameObject);
        }
        else
        {
            GetComponent<PersonAI>().DroppedOn(UnderCursor);
        }
        dragging = false;
    }

}
