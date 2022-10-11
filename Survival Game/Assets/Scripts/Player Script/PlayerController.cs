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
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Transform ground;
    [SerializeField] private float groundDistance = 0.1f;
    [SerializeField] private LayerMask groundmask;

    [Header("Crouch:")]
    [SerializeField] private float CrouchMultiplySpeed = 0.5f;


    [Header("Colider variables: ")]
    [SerializeField] private PlayerColider coliderCrouch;

    private PlayerColider coliderBase = new();

    [Header("Test:")]
    [SerializeField] bool isGrounded;
    [SerializeField] private Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] private float totalSpeed;
    private CharacterController characterController;
    private InputManager inputManager;
    private Animator animator;
    private bool hasAnimator;
    private int xVelAnimator;
    private int yVelAnimator;
    private float xRotation;
    private int jumpAnimator;
    private int fallAnimator;
    private int crouchAnimator;

    private float speed = 5f;
    private Vector2 currentVelocity;

    private void Awake()
    {
        hasAnimator = TryGetComponent<Animator>(out animator);
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        xVelAnimator = Animator.StringToHash("X_Velocity");
        yVelAnimator = Animator.StringToHash("Y_Velocity");
        jumpAnimator = Animator.StringToHash("Jump");
        fallAnimator = Animator.StringToHash("Falling");
        crouchAnimator = Animator.StringToHash("Crouch");
    }
    private void Start()
    {
        totalSpeed = speed;
        GetCopyFromCharacter(coliderBase);
    }
    private void Update()
    {
        Move();
        CameraMovements();
        IsGrounded();
        
    }

    private void Move()
    {
        if(!hasAnimator) { return; }
        if (inputManager.Crouch) totalSpeed = speed * CrouchMultiplySpeed;
        else if (inputManager.Run) totalSpeed = speed * RunMultiplySpeed;
        else totalSpeed = speed;
        HorizontalMove();
        Jump();
        CrouchHandle();
        var inputDirection = new Vector3(inputManager.Move.x, inputManager.Move.y).normalized;
        currentVelocity.x = Mathf.Lerp(currentVelocity.x, totalSpeed * inputDirection.x, animationSpeed * Time.deltaTime);
        currentVelocity.y = Mathf.Lerp(currentVelocity.y, totalSpeed * inputDirection.y, animationSpeed * Time.deltaTime);
        animator.SetFloat(xVelAnimator, currentVelocity.x);
        animator.SetFloat(yVelAnimator, currentVelocity.y);
    }

    private void CameraMovements()
    {
        if(!hasAnimator) { return; }
        Camera.position = CameraRoot.position;
        xRotation -= inputManager.Look.y * Time.smoothDeltaTime * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, CameraAngle.x, CameraAngle.y);
        Camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up, inputManager.Look.x * Sensitivity * Time.smoothDeltaTime);
    }

    private void HorizontalMove()
    {
        Vector3 horizontalMove = transform.right * inputManager.Move.x + transform.forward * inputManager.Move.y;
        horizontalMove = totalSpeed * Time.deltaTime * horizontalMove.normalized;
        characterController.Move(horizontalMove);
    }


    private void GravityDrop()
    {
        if (inputManager.Crouch) inputManager.Crouch = false;
        verticalVelocity.y += gravity * Time.deltaTime;
        characterController.Move(verticalVelocity * Time.deltaTime);
        animator.SetBool(fallAnimator, true);
        
    }

    private void Jump()
    {
        if (inputManager.Jump && isGrounded)
        {
            if (inputManager.Crouch) inputManager.Crouch = false;
            animator.SetTrigger(jumpAnimator);
            inputManager.Jump = false;
            
        }
    }

    private void IsGrounded()
    {
        isGrounded = Physics.CheckSphere(ground.position, groundDistance, groundmask);
        if(!isGrounded)
        {
            GravityDrop();
            
        }
        else
        {
            verticalVelocity.y = 0f;
            if(animator.GetBool(fallAnimator))
            {
                animator.SetBool(fallAnimator, false);
            }
        }
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ground.position, groundDistance);
        
    }

    private void CrouchHandle()
    {
        animator.SetBool(crouchAnimator, inputManager.Crouch);
    }

    private void GetCopyFromCharacter(PlayerColider playerColider)
    {
        playerColider.Center = characterController.center;
        playerColider.Height = characterController.height;
        playerColider.Radius = characterController.radius;
    }

    private void AdjustmentColider(PlayerColider playerColider)
    {
        characterController.center = playerColider.Center;
        characterController.height = playerColider.Height;
        characterController.radius = playerColider.Radius;
    }

    public void AddVerticalVelocity()
    {
        verticalVelocity.y = Mathf.Sqrt(-2 * jumpHeight * gravity);
    }

    public void AdjustColiderToCrouch()
    {
        AdjustmentColider(coliderCrouch);
    }

    public void AdjustColiderToBase()
    {
        AdjustmentColider(coliderBase);
    }


}
