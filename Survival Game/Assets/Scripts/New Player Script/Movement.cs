using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 11f;
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpHeight = 2f;

    [Header("Run: ")]
    [SerializeField] private float RunMultiply = 1.5f;

    private bool isGrounded;
    private Vector3 verticalVelocity = Vector3.zero;
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
        float multiply = Run();
        Vector3 horizontalMove = transform.right * inputManager.Move.x + transform.forward * inputManager.Move.y;
        horizontalMove = speed * Time.deltaTime * horizontalMove.normalized * multiply;
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
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
        }
    }

    private float Run()
    {
        if(inputManager.Run)
        {
            float run = RunMultiply;
            return run;
        }
        else
        {
            return 1f;
        }
    }
}
