using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Animation: ")]
    [SerializeField] private float animationSpeed = 8.9f;

    [Header("Camera: ")]
    [SerializeField] private Transform CameraRoot;
    [SerializeField] private Transform Camera;
    [SerializeField] private Vector2 CameraAngle = new(-40f, 70f);
    [SerializeField] private float Sensitivity = 20f;

    [Header("Run:")]
    [SerializeField] private float RunMultiplySpeed = 1.5f;

    [Header("Jump:")]
    [SerializeField] private float gravity = -15f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Transform ground;
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField] private LayerMask groundmask;

    [Header("Crouch:")]
    [SerializeField] private float CrouchMultiplySpeed = 0.5f;

    [SerializeField] bool isGrounded;
    private Vector3 verticalVelocity = Vector3.zero;
    private CharacterController characterController;
    private InputManager inputManager;
    private Animator animator;
    private bool hasAnimator;
    private int xVelAnimator;
    private int yVelAnimator;
    private float xRotation;

    private float speed = 5f;
    private Vector2 currentVelocity;

    private void Awake()
    {
        hasAnimator = TryGetComponent<Animator>(out animator);
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        xVelAnimator = Animator.StringToHash("X_Velocity");
        yVelAnimator = Animator.StringToHash("Y_Velocity");
        
    }

    private void Update()
    {
        Move();
        isGrounded = IsGrounded();
    }

    private void LateUpdate()
    {
        CameraMovements();
    }


    private void Move()
    {
        if(!hasAnimator) { return; }

        float targetSpeed = Run() * Crouch() * speed;
        HorizontalMove();
        Jump();
        GravityDrop();
        var inputDirection = new Vector3(inputManager.Move.x, inputManager.Move.y).normalized;
        currentVelocity.x = Mathf.Lerp(currentVelocity.x, targetSpeed * inputDirection.x, animationSpeed * Time.deltaTime);
        currentVelocity.y = Mathf.Lerp(currentVelocity.y, targetSpeed * inputDirection.y, animationSpeed * Time.deltaTime);
        animator.SetFloat(xVelAnimator, currentVelocity.x);
        animator.SetFloat(yVelAnimator, currentVelocity.y);
    }

    private void CameraMovements()
    {
        if(!hasAnimator) { return; }
        Camera.position = CameraRoot.position;
        xRotation -= inputManager.Look.y * Time.deltaTime * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, CameraAngle.x, CameraAngle.y);
        Camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up, inputManager.Look.x * Sensitivity * Time.deltaTime);
    }

    private void HorizontalMove()
    {
        float totalSpeed = Run() * Crouch() * speed;
        Vector3 horizontalMove = transform.right * inputManager.Move.x + transform.forward * inputManager.Move.y;
        horizontalMove = totalSpeed * Time.deltaTime * horizontalMove.normalized;
        characterController.Move(horizontalMove);
        //AdjustmentCrouch();
    }

    private float Run()
    {
        if (inputManager.Run)
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

    private void GravityDrop()
    {
        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (inputManager.Jump  && isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(ground.position, groundDistance, groundmask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ground.position, groundDistance);
        
    }

}
