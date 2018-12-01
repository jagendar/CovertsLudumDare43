using System;
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

        [SerializeField] float rotationTime;
        
        private bool rotating;
        
        private void Update()
        {
            if (rotating) return;

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
    }
}