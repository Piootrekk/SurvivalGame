using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class ExtraClickButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UI_InventoryManager inventoryManager;
    private Keyboard keyboard;
    private UI_Slot currentSlotData;
    private bool isKeyButtonPressed;
    private bool isEnter;
    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<UI_InventoryManager>();
        keyboard = InputSystem.GetDevice<Keyboard>();
        currentSlotData = GetComponent<UI_Slot>();
    }
    private void Update()
    {
        CheckQPressed();
    if (!BuildingSystem.Instance.IsExecuting) CheckGPressed();
    
    }

    private void CheckQPressed()
    {
        if (keyboard.qKey.wasPressedThisFrame)
        {
            isKeyButtonPressed = true;
        }
        else
        {
            isKeyButtonPressed = false;
        }
        if (isKeyButtonPressed && isEnter)
        {
            inventoryManager.DropCurrentItem();
        }
    }

    private void CheckGPressed()
    {
        if (keyboard.gKey.wasPressedThisFrame)
        {
            isKeyButtonPressed = true;
        }
        else
        {
            isKeyButtonPressed = false;
        }
        if (isKeyButtonPressed && isEnter)
        {
            inventoryManager.UseCurrentItem();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventoryManager.GetItemToHandlerRightClick();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetID();
        SetData();
        isEnter = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
        inventoryManager.EmptyDataPanel();
    }

    public void SetID()
    {
        inventoryManager.CurrentSlot = currentSlotData.ID;
    }

    public void SetData()
    {
        inventoryManager.GetItemData();
    }

}
