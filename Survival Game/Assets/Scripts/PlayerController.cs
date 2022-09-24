using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float animationSpeed = 8.9f;

    [Header("Camera: ")]
    [SerializeField] private Transform CameraRoot;
    [SerializeField] private Transform Camera;
    [SerializeField] private Vector2 CameraAngle = new Vector2(-40f, 70f);
    [SerializeField] private float Sensitivity = 20f;

    private Rigidbody rb;
    private InputManager inputManager;
    private Animator animator;
    private bool hasAnimator;
    private int xVelAnimator;
    private int yVelAnimator;
    private float xRotation;

    private const float speed = 2f;
    private const float runSpeed = 6f;
    private Vector2 currentVelocity;

    private void Start()
    {
        hasAnimator = TryGetComponent<Animator>(out animator);
        rb = GetComponent<Rigidbody>();
        inputManager = GetComponent<InputManager>();
        xVelAnimator = Animator.StringToHash("X_Velocity");
        yVelAnimator = Animator.StringToHash("Y_Velocity");
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraMovements();
    }


    private void Move()
    {
        if(!hasAnimator) { return; }
        float targetSpeed;
        if (inputManager.Run)
        {
            targetSpeed = runSpeed;
        }
        else
        {
            targetSpeed = speed;
        }

        if (inputManager.Move == Vector2.zero)
        {
            targetSpeed = 0.1f;
        }
        var inputDirection = new Vector3(inputManager.Move.x, inputManager.Move.y).normalized;
        currentVelocity.x = Mathf.Lerp(currentVelocity.x, targetSpeed * inputDirection.x, animationSpeed * Time.fixedDeltaTime);
        currentVelocity.y = Mathf.Lerp(currentVelocity.y, targetSpeed * inputDirection.y, animationSpeed * Time.fixedDeltaTime);

        var xVelDiff = currentVelocity.x - rb.velocity.x;
        var zVelDiff = currentVelocity.y - rb.velocity.z;

        rb.AddForce(transform.TransformVector(new Vector3(xVelDiff, 0, zVelDiff)), ForceMode.VelocityChange);

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

}
