﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.UserInput
{
    public class CameraControl : MonoBehaviour
    {
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
            transform.position += worldMovement;
        }
    }
}