using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Dodaæ kursor, wy³¹czyæ poruszanie siê myszk¹, dodaæ aktywacjê panelu pod e
public class UI_InventoryManager : MonoBehaviour
{
    [SerializeField] private Transform inventorySlotHolder;
    [SerializeField] private Transform inventoryHotBarSlotHolder;

    [SerializeField] private List<UI_InventorySlot> inventorySlots;
    [SerializeField] private List<UI_InventorySlot> inventoryHotBarSlots;

    [SerializeField] private int currentSlot;
    [SerializeField] private Vector2 offset;

    private Transform cursor;

    public int CurrentSlot { get => currentSlot; set => currentSlot = value; }

    private void Awake()
    {
        SetSlotsForInventory();
        SetSlotsForHotBar();
        SetSlotsID();
        CheckIfSlotIsFull(inventorySlots);
        CheckIfSlotIsFull(inventoryHotBarSlots);
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
            inventoryHotBarSlots.Add(new(slot, false));
        }
    }

    private void CheckIfSlotIsFull(List<UI_InventorySlot> Slots)
    {
        foreach(UI_InventorySlot slot in Slots)
        {
            
            if(slot.Slot.childCount > 0)
            {
                slot.IsFull = true;
            }
            else slot.IsFull = false;
        }
    }

    private void ItemAdd(GameObject item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            foreach (UI_InventorySlot slot in inventorySlots)
            {
                if (!slot.IsFull)
                {
                    Instantiate(item, slot.Slot);
                    CheckIfSlotIsFull(inventorySlots);
                    return;
                }
                else Debug.Log("Chuj nie dzia³a");
            }
        }
        Debug.Log("Full Inventory");
    }

    private void SetSlotsID()
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].Slot.GetComponent<UI_Slot>()) return;
            inventorySlots[i].Slot.GetComponent<UI_Slot>().ID = i;
        }
    }


}




[System.Serializable]
public class UI_InventorySlot
{
    [SerializeField] private Transform slot;
    [SerializeField] private bool isFull;
    
    public bool IsFull { get => isFull; set => isFull = value; }
    public Transform Slot { get => slot; set => slot = value; }


    public UI_InventorySlot(Transform slot, bool isFull)
    {
        this.slot = slot;
        this.isFull = isFull;
    }
}