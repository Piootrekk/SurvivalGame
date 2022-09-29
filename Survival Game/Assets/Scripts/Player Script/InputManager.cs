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
    public bool Jump { get; private set; }

    private InputActionMap currentMap;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction runAction;
    private InputAction jumpAction;

    private void Awake()
    {
        currentMap = playerInput.currentActionMap;
        moveAction = currentMap.FindAction("Move");
        lookAction = currentMap.FindAction("Look");
        runAction = currentMap.FindAction("Run");
        jumpAction = currentMap.FindAction("Jump");

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
        moveAction.performed += OnMove;
        lookAction.performed += OnLook;
        runAction.performed += OnRun;
        jumpAction.performed += OnJump;
    }

    private void StopPerforAction()
    {
        moveAction.canceled += OnMove;
        lookAction.canceled += OnLook;
        runAction.canceled += OnRun;
        jumpAction.canceled += OnJump;
    }

    private void OnLook(InputAction.CallbackContext callBack)
    {
        Look = callBack.ReadValue<Vector2>();
    }

    private void OnMove(InputAction.CallbackContext callBack)
    {
        Move = callBack.ReadValue<Vector2>();
    }

    private void OnRun(InputAction.CallbackContext callBack)
    {
        Run = callBack.ReadValueAsButton();
    }

    private void OnJump(InputAction.CallbackContext callBack)
    {
        Jump = callBack.ReadValueAsButton();
        Debug.Log("Zajebano spacjê");
    }




}
