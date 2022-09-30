using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Test:")]
    [SerializeField] private float totalSpeed;

    [SerializeField] private float speed = 11f;
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpHeight = 2f;

    [Header("Run:")]
    [SerializeField] private float RunMultiplySpeed = 1.5f;

    [Header("Crouch:")]
    [SerializeField] private float CrouchMultiplySpeed = 0.5f;
    [SerializeField] private float crouchHeighMultiply = 0.5f;

    
    private bool isGrounded;
    private Vector3 verticalVelocity = Vector3.zero;
    private InputManager inputManager;
    private CharacterController characterController;
    private float standHeight;
    private float crouchHeight;
    private Transform playerBody;
    private readonly float crouchTransitionSpeed = 10f;
    private float currentHeight;
    private Vector3 initialCameraPosition;
    private Transform cameraTransform;
    private Vector3 bodyScale;

    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        playerBody = transform.GetChild(0);
        cameraTransform = transform.GetChild(1);
        
    }

    public void Start()
    {
        standHeight = characterController.height;
        currentHeight = standHeight;
        crouchHeight = characterController.height * crouchHeighMultiply;
        initialCameraPosition = transform.GetChild(1).localPosition;
        bodyScale = playerBody.localScale;
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
        totalSpeed = Run() * Crouch() * speed;
        Vector3 horizontalMove = transform.right * inputManager.Move.x + transform.forward * inputManager.Move.y;
        horizontalMove = totalSpeed * Time.deltaTime * horizontalMove.normalized;
        characterController.Move(horizontalMove);
        AdjustmentCrouch();
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
            return RunMultiplySpeed;
        }
        else return 1f;
    }

    private float Crouch()
    {
        if (inputManager.Crouch)
        {
            return CrouchMultiplySpeed;
        }
        else return 1f;
    }

    private void AdjustmentCrouch()
    {
        var heightTarget = inputManager.Crouch ? crouchHeight : standHeight;

        currentHeight = Mathf.Lerp(currentHeight, heightTarget, Time.deltaTime * crouchTransitionSpeed);

        var halfHeightDifference = new Vector3(0, (standHeight - currentHeight) / 2, 0);
        var newCameraPosition = initialCameraPosition - halfHeightDifference;
        cameraTransform.localPosition = newCameraPosition;

        var newHeightForBody = bodyScale - halfHeightDifference;
        playerBody.localScale = newHeightForBody;

        characterController.height = currentHeight;
    }

}
