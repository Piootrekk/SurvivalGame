using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }

    private InputActionMap currentMap;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction runAction;

    private void Awake()
    {
        currentMap = playerInput.currentActionMap;
        moveAction = currentMap.FindAction("Move");
        lookAction = currentMap.FindAction("Look");
        runAction = currentMap.FindAction("Run");

        PerforAction();
        StopPerforAction();

    }

    private void OnEnable()
    {
        currentMap.Enable();
    }

    private void OnDisable()
    {
        currentMap.Disable();
    }



    private void PerforAction()
    {
        moveAction.performed += onMove;
        lookAction.performed += onLook;
        runAction.performed += onRun;
    }

    private void StopPerforAction()
    {
        moveAction.canceled += onMove;
        lookAction.canceled += onLook;
        runAction.canceled += onRun;
    }

    private void onLook(InputAction.CallbackContext callBack)
    {
        Look = callBack.ReadValue<Vector2>();
    }

    private void onMove(InputAction.CallbackContext callBack)
    {
        Move = callBack.ReadValue<Vector2>();
    }

    private void onRun(InputAction.CallbackContext callBack)
    {
        Run = callBack.ReadValueAsButton();
    }




}
