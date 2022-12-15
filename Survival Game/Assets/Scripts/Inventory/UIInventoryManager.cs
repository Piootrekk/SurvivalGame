using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private Transform inventorySlotHolder;
    [SerializeField] private Transform inventoryHotBarSlotHolder;

    [SerializeField] private List<UI_InventorySlot> inventorySlots;
    [SerializeField] private List<UI_InventorySlot> inventoryHotBarSlots;

    private void Start()
    {
        SetSlotsForInventory();
        SetSlotsForHotBar();
    }
    private void SetSlotsForInventory()
    {
        foreach(Transform slot in inventorySlotHolder)
        {
            if (slot == null) return;
            inventorySlots.Add(new(slot, false));
        }
    }

    private void SetSlotsForHotBar()
    {
        foreach(Transform slot in inventoryHotBarSlotHolder)
        {
            if (slot == null) return;
            inventorySlots.Add(new(slot, false));
        }
    }

}




[System.Serializable]
public class UI_InventorySlot
{
    [SerializeField] private Transform slot;
    [SerializeField] private bool isFull;
    
    public Transform Slot => slot;
    public bool IsFull => isFull;

    public UI_InventorySlot(Transform slot, bool isFull)
    {
        this.slot = slot;
        this.isFull = isFull;
    }
}