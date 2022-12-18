using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotBarSlots : MonoBehaviour, IChangeBar
{
    private InputManager inputManager;

    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }


    public void HotBarChange()
    {
        if (inputManager == null) { return; }
        Debug.Log(inputManager.HotBarKeyChange);

    }
}
