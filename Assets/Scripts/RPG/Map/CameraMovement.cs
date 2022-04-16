using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraMaxX;
    [SerializeField] private float cameraMinX;
    [SerializeField] private float cameraMaxY;
    [SerializeField] private float cameraMinY;
    private void Update()
    {
        if (transform.position != Player.transform.position)
        {
            float cameraX = Math.Max(Math.Min(cameraMaxX, Player.transform.position.x),cameraMinX);
            float cameraY = Math.Max(Math.Min(cameraMaxY, Player.transform.position.y),cameraMinY);
            Vector3 targetPosition = new Vector3(cameraX, cameraY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraMoveSpeed);
        }
    }
}
