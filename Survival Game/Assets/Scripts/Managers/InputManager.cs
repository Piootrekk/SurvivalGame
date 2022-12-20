using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Interactions;


public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public bool Run { get; private set; }
    public bool Jump { get; set; }
    public bool Crouch { get; set; }
    public float Test { get; private set; }
    public bool Interactive { get; private set; }
    public float HotBarKey { get; private set; }
    public bool Inventory { get; set; }


    public string CurrentPathInput { get; private set; }

    private InputActionMap currentMap;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction runAction;
    private InputAction jumpAction;
    private InputAction crouchAction;
    private InputAction testAction;
    private InputAction interactive;
    private InputAction hotbarkeys;
    private InputAction inventoryAction;




    private void Awake()
    {
        currentMap = playerInput.currentActionMap;
        moveAction = currentMap.FindAction("Move");
        lookAction = currentMap.FindAction("Look");
        runAction = currentMap.FindAction("Run");
        jumpAction = currentMap.FindAction("Jump");
        crouchAction = currentMap.FindAction("Crouch");
        testAction = currentMap.FindAction("TestDamage");
        interactive = currentMap.FindAction("Interactive");
        hotbarkeys = currentMap.FindAction("HotBarKeys");
        inventoryAction = currentMap.FindAction("OpenInventory");
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
        jumpAction.started += OnJump;
        crouchAction.performed += OnCrouch;
        testAction.started += OnTest;
        interactive.started += OnInteractive;
        hotbarkeys.performed += OnChangeHotBarSlot;
        inventoryAction.started += OnInventory;
    }



    private void StopPerforAction()
    {
        moveAction.canceled += OnMove;
        lookAction.canceled += OnLook;
        runAction.canceled += OnRun;
        jumpAction.canceled += OnJump;
        crouchAction.canceled += OnCrouch;
        testAction.canceled += OnTest;
        interactive.canceled += OnInteractive;
        hotbarkeys.canceled += OnChangeHotBarSlot;
        inventoryAction.canceled += OnInventory;
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
        if (callBack.interaction is TapInteraction)
        {
           Jump = callBack.ReadValueAsButton();
        }
    }


    private void OnCrouch(InputAction.CallbackContext callBack)
    {
        if(callBack.control.IsPressed(0.5f))
        {
            Crouch = !Crouch;
        }
    }

    private void OnTest(InputAction.CallbackContext callBack)
    {
        if (callBack.interaction is TapInteraction)
        {
            Test = callBack.ReadValue<float>();
            Debug.Log(Test);
        }
    }

    private void OnInteractive(InputAction.CallbackContext callBack)
    {
        Interactive = callBack.ReadValueAsButton();
        CurrentPathInput = GetPathFromInputAction(callBack);
    }

    public void OnChangeHotBarSlot(InputAction.CallbackContext callBack)
    {
        if (callBack.ReadValue<float>() > 0f)
        {
            HotBarKey = callBack.ReadValue<float>();
            Debug.Log(HotBarKey);
        }
        
    }


    public string GetPathFromInputAction(InputAction.CallbackContext input)
    {
        return ((KeyControl)input.control).keyCode.ToString();
    }

    private void OnInventory(InputAction.CallbackContext callBack)
    {
        if (callBack.control.IsPressed(0.5f))
        {
            
            Inventory = !Inventory;
        }
    }


}





public interface IChangeBar
{
    void HotBarChange();
}