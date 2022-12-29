using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private HotBarSlots hotBarSlots;
    private InputManager inputManager;
    private bool isExecuting;
    private float delay = 1f;
    private float timer = 0f;
    private void Awake()
    {
        hotBarSlots = GetComponent<HotBarSlots>();
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
       CheckIfActive();
    }

    private void CheckIfActive()
    {
        if (inputManager.Mouse1 && hotBarSlots.IsItemInSlotHotBar((int)inputManager.HotBarKey - 1) 
            && hotBarSlots.IsItemEquipable((int)inputManager.HotBarKey - 1) && !isExecuting && !inputManager.Inventory)
        {
            inputManager.Mouse1 = !inputManager.Mouse1;
            AnimationGetter();
            isExecuting = true;
            timer = Time.time;
        }
        else if (Time.time - timer > delay)
        {
            isExecuting = false;
        }
    }

    private void AnimationGetter()
    {
        IAnimation iAnimate = GameObject.FindObjectOfType<OnAttackAnimations>();
        if (iAnimate == null) return;
        iAnimate?.Animate();
    }
    
}
