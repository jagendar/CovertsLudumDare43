using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDrag : MonoBehaviour {
 
    private Camera cam;
    public GameObject UnderCursor;
    private bool dragging;
    private bool overVolcano;

	void Start ()
    {
        cam = Camera.main;
        overVolcano = false;
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

        if (Physics.Raycast(ray, out hit, 99999, ~LayerMask.NameToLayer("Tiles")))
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
    }

    private void OnMouseUp()
    {
        if (UnderCursor.name == "Dirt" || UnderCursor.name == "Lava")
        {
            Destroy(this.gameObject);
        }
        dragging = false;
    }

}
