using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 11f;
    [SerializeField] private float gravity = -15f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight = 3.5f;

    [SerializeField] private bool isGrounded;
    private Vector3 verticalVelocity = Vector3.zero;
    private Vector3 transformPositionForCheck;
    private InputManager inputManager;
    private CharacterController characterController;
    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        IsGrounded();
        HorizontalMove();
        Jump();
        GravityDrop();

    }



    private void HorizontalMove()
    {
        Vector3 horizontalMove = transform.right * inputManager.Move.x + transform.forward * inputManager.Move.y;
        //horizontalMove = speed * Time.deltaTime * horizontalMove.normalized;;
        horizontalMove *= speed * Time.deltaTime;
        characterController.Move(horizontalMove);

    }

    private void IsGrounded()
    {
        if (characterController.isGrounded)
        {
            isGrounded = true;
            verticalVelocity.y = 0f;
        }
        else isGrounded = false;
    }

    private void GravityDrop()
    {
        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (inputManager.Jump && isGrounded)
        {
            Debug.Log("Skoczono");
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
        }
    }
}
