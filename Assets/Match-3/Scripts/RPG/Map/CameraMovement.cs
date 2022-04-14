using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float cameraMoveSpeed;
    private void Update()
    {
        if (transform.position != Player.transform.position)
        {
            Vector3 targetPosition = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, cameraMoveSpeed);
        }
    }
}
