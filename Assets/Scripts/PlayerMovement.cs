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
    private Camera playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>();
    }
    private void FixedUpdate()
    {
        Move();
        //LookMouseCursor();
    }
    private void Move()
    {
        Vector3 moveDistance = ((playerInput.move * transform.forward) + (playerInput.rotate * transform.right)) * Time.deltaTime * moveSpeed;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDistance);
        playerAnimator.SetFloat("Y", playerInput.move);
        playerAnimator.SetFloat("X", playerInput.rotate);
    }
    void LookMouseCursor()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitResult;
        if(Physics.Raycast(ray, out hitResult))
        {
            Vector3 mouseDir = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
            playerAnimator.transform.forward = mouseDir;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IItem item = other.GetComponent<IItem>();
        if(item != null)
        {
            item.Use(gameObject);
        }
    }
}
