using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 180f;

    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Move();
        playerAnimator.SetFloat("Y", playerInput.move);
        playerAnimator.SetFloat("X", playerInput.rotate);
    }
    private void Move()
    {
        Vector3 moveDistance = ((playerInput.move * transform.forward) + (playerInput.rotate * transform.right)) * Time.deltaTime * moveSpeed;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        
    }
}
