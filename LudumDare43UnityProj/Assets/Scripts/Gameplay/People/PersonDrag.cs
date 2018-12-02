using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDrag : MonoBehaviour {
    [SerializeField] private Camera cam;
    private bool dragging;

	void Start ()
    {
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
            int hitX = hit.point.x;
            int hitZ = hit.point.z;
            int hitY = hit.point.y;


            Vector3 hitPos = new Vector3(hitX, hitY, hitZ) + hit.normal * 5;
            Quaternion hitRot = Quaternion.identity;

            if (hit.normal.x == -1 || hit.normal.x == 1)
            {
                hitRot = Quaternion.Euler(0, 90, 0);
            }

            this.transform.position = hitPos;
        }
        else
            Cursor.visible = true;
    }


    private void OnMouseDown()
    {
        dragging = true;    
    }

}
