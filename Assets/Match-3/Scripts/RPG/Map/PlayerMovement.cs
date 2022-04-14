using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int playerMoveSpeed;
    [SerializeField] private VJHandler vjHandler;
    [SerializeField] private bool isVJ;
    private Vector3 change;
    private Rigidbody2D playerRigidbody;
    private Animator Animator;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isVJ)
        {
            change = Vector3.zero;
            change.x = vjHandler.InputDirection.x;
            change.y = vjHandler.InputDirection.y;
            change = change.normalized;
        }
        else
        {
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            change = change.normalized;
        }
        
        if (change != Vector3.zero)
        {
            MovePlayer();
            Animator.SetFloat("moveX", change.x);
            Animator.SetFloat("moveY", change.y);
            Animator.SetBool("isMoving", true);
        }
        else
            Animator.SetBool("isMoving", false);
            
    }

    void MovePlayer()
    {
        playerRigidbody.MovePosition(transform.position + change*playerMoveSpeed * Time.fixedDeltaTime);
    }
}
