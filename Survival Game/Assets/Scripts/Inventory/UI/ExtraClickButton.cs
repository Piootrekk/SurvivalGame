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
    private bool isQpress;
    private bool isEnter;
    private float timer = 0f;
    private readonly float delay = 0.3f;
    private bool actionPerformed = false;
    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<UI_InventoryManager>();
        keyboard = InputSystem.GetDevice<Keyboard>();
        currentSlotData = GetComponent<UI_Slot>();
    }
    private void Update()
    {
        if (keyboard.qKey.wasPressedThisFrame)
        {
            isQpress = true;
        }
        else
        {
            isQpress = false;
        }
        if (isQpress && isEnter && timer >= delay && !actionPerformed)
        {
            Time.timeScale = 0;
            actionPerformed = true;
            timer = 0f;
            inventoryManager.DropCurrentItem();
            Time.timeScale = 1;
        }
        else if (!isQpress && !isEnter)
        {
            actionPerformed = false;
        }
        timer += Time.deltaTime;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("HEUEHEH");
            //TODO add handle single item
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetID();
        isEnter = true;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isEnter = false;
    }

    public void SetID()
    {
        inventoryManager.CurrentSlot = currentSlotData.ID;
    }

}
