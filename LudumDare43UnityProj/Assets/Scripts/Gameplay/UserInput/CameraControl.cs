using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class CameraControl : MonoBehaviour
    {
        [Header("Pan")]
        [SerializeField] Vector3 minCamera;
        [SerializeField] Vector3 maxCamera;
        [SerializeField] float panSpeed = 1;

        [Header("Rotation")]
        [SerializeField] float rotationTime;

        [Header("Zoom")]
        [SerializeField] float zoomAmount;
        [SerializeField] float zoomMin;
        [SerializeField] float zoomMax;

        [Header("Object References")]
        [SerializeField] LayerMask rotateLayer;
        [SerializeField] Camera controlledCam;

        private bool rotating;
        private float currentZoom;
        
        private void Update()
        {
            if (rotating) return;

            Vector3 localMovement = new Vector2();
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                localMovement.z += panSpeed;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                localMovement.z -= panSpeed;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                localMovement.x -= panSpeed;
            }
            if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                localMovement.x += panSpeed;
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

            if(Input.mouseScrollDelta.y > 0 || Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Zoom(-zoomAmount);
            }

            if (Input.mouseScrollDelta.y < 0 || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                Zoom(zoomAmount);
            }
        }

        void Rotate(float degrees)
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(controlledCam.transform.position, controlledCam.transform.forward, out hitInfo, float.MaxValue, rotateLayer))
            {
                Vector3 pos = transform.position;
                Quaternion rot = transform.rotation;
                transform.RotateAround(hitInfo.point, Vector3.up, degrees);
                StartCoroutine(RotationLerp(pos, rot, transform.position, transform.rotation));
            }
        }

        private IEnumerator RotationLerp(Vector3 startPos, Quaternion startRot, Vector3 endPos, Quaternion endRot)
        {
            rotating = true;
            for (float t = 0; t < rotationTime; t += Time.deltaTime)
            {
                RotationLerp(startPos, startRot, endPos, endRot, t / rotationTime);
                yield return null;
            }
            RotationLerp(startPos, startRot, endPos, endRot, 1);

            rotating = false;
        }

        private void RotationLerp(Vector3 pos1, Quaternion rot1, Vector3 pos2, Quaternion rot2, float percentage)
        {
            transform.position = Vector3.Lerp(pos1, pos2, percentage);
            transform.rotation = Quaternion.Slerp(rot1, rot2, percentage);
        }

        private void Zoom(float amount)
        {
            controlledCam.orthographicSize = Mathf.Clamp(controlledCam.orthographicSize + amount, zoomMin, zoomMax);
        }
    }
}