using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] LayerMask rotateLayer;
        [SerializeField] Camera controlledCam;
        [SerializeField] Vector3 minCamera;
        [SerializeField] Vector3 maxCamera;
        
        private void Update()
        {
            Vector3 localMovement = new Vector2();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                localMovement.z += 1;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                localMovement.z -= 1;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                localMovement.x -= 1;
            }
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                localMovement.x += 1;
            }
            Vector3 worldMovement = transform.TransformDirection(localMovement);
            transform.position = MathfExtensions.Clamp(worldMovement + transform.position, minCamera, maxCamera);

            if(Input.GetKeyDown(KeyCode.Q))
            {
                Rotate(-90);
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                Rotate(90);
            }
        }

        void Rotate(float degrees)
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(controlledCam.transform.position, controlledCam.transform.forward, out hitInfo, float.MaxValue, rotateLayer))
            {
                transform.RotateAround(hitInfo.point, Vector3.up, degrees);
            }
        }
    }
}