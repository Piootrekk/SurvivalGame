using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] float sensitivity = 8f;
    [SerializeField] bool reverse = false;
    [SerializeField] Vector2 CameraClamp = new(-90f, 80f);

    private CharacterController characterController;
    private Transform playerCamera;
    private float xRotation = 0f;
    private InputManager inputManager;
    private Vector3 cameraPosition;

    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerCamera = transform.GetChild(1);
        cameraPosition = playerCamera.localPosition;
        characterController = GetComponent<CharacterController>();
    }


    private void LateUpdate()
    {
        RotateCameraLook();
    }

    private void RotateCameraLook()
    {
        
        if (reverse)
        {
            xRotation += inputManager.Look.y * Time.deltaTime * sensitivity;
        }
        else
        {
            xRotation -= inputManager.Look.y * Time.deltaTime * sensitivity;
        }
        xRotation = Mathf.Clamp(xRotation, CameraClamp.x, CameraClamp.y);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up, inputManager.Look.x * sensitivity * Time.deltaTime);

    }

    //private void AdjustmentCameraLook()
    //{
    //    if (inputManager.Crouch)
    //    {
    //        playerCamera.localPosition = new(0f, 0f, 0f);
    //    }
    //    else playerCamera.localPosition = cameraPosition;
    //}

}
