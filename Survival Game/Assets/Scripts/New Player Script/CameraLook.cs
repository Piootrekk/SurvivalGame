using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    [SerializeField] float sensitivityHorizontal = 8f;
    [SerializeField] float sensitivityVertical = 8f;
    [SerializeField] bool reverse = false;
    [SerializeField] Vector2 CameraClamp = new(-90f, 80f);

    private Transform playerCamera;
    private float xRotation = 0f;
    private InputManager inputManager;

    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerCamera = transform.GetChild(1);
    }


    private void LateUpdate()
    {
        RotateCameraLook();
    }

    private void RotateCameraLook()
    {
        
        if (reverse)
        {
            xRotation += inputManager.Look.y * Time.deltaTime * sensitivityVertical;
        }
        else
        {
            xRotation -= inputManager.Look.y * Time.deltaTime * sensitivityVertical;
        }
        xRotation = Mathf.Clamp(xRotation, CameraClamp.x, CameraClamp.y);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up, inputManager.Look.x * sensitivityHorizontal * Time.deltaTime);

    }
}
