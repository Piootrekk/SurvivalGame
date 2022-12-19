using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UI_InventoryManager : MonoBehaviour
{
    [SerializeField] private Transform inventorySlotHolder;
    [SerializeField] private Transform inventoryHotBarSlotHolder;

    [SerializeField] private List<UI_InventorySlot> inventorySlots;

    [SerializeField] private int currentSlot;
    [SerializeField] private Vector2 offset;
    [SerializeField] private Transform cursor;

    [SerializeField] bool isCursorWithItem;    
    private GameObject inventory;

    public int CurrentSlot { get => currentSlot; set => currentSlot = value; }
    public bool IsCursorWithItem => isCursorWithItem;

    private void Awake()
    {
        inventory = inventorySlotHolder.transform.parent.gameObject;
        SetSlotsForInventory();
        SetSlotsForHotBar();
        SetSlotsID();
        CheckIfSlotIsFull();
    }

    private void Update()
    {
        ItemHandler();
        ItemHandlerVisibility();
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

    private void CheckIfSlotIsFull()
    {
        foreach(UI_InventorySlot slot in inventorySlots)
        {
            
            if(slot.Slot.childCount > 0)
            {
                slot.IsFull = true;
            }
            else slot.IsFull = false;
        }
    }

    private bool FindSameItemInIventory(UI_InventorySlot slot, GameObject itemObject)
    {
        var item = itemObject.GetComponent<ItemObjectInGame>().InstanceInInventory;
        if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId == item.GetComponent<UI_ItemData>().ItemData.ItemId)
        {
            int amount = slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
            int amountInObject = itemObject.GetComponent<ItemObjectInGame>().Amount;
            int maxStack = item.GetComponent<UI_ItemData>().ItemData.StackLimit;
            if (amount + amountInObject <= maxStack)
            {
                slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount
                    += amountInObject;
                slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                return true;
            }
            else return false;
        }
        return false;
    }

    public bool ItemAdd(GameObject itemObject)
    {
        CheckIfSlotIsFull();
        var item = itemObject.GetComponent<ItemObjectInGame>().InstanceInInventory;
        foreach (UI_InventorySlot slot in inventorySlots)
        {
            if (slot.IsFull && FindSameItemInIventory(slot, itemObject))
            {
                CheckIfSlotIsFull();
                return true;
            }
        }
        foreach (UI_InventorySlot slot in inventorySlots)
        {
            Instantiate(item, slot.Slot);
            slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
            CheckIfSlotIsFull();
            return true;
        }
        Debug.Log("Full Inventory");
        return false;
    }

    private void SetSlotsID()
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].Slot.GetComponent<UI_Slot>()) return;
            inventorySlots[i].Slot.GetComponent<UI_Slot>().ID = i;
        }
    }

    private void ItemHandler()
    {
        if (!inventory.activeSelf) return;
        cursor.position = Mouse.current.position.ReadValue() + offset;
    }

    public void GetItemToHandler()
    {
        if (inventorySlots[currentSlot].Slot.childCount > 0 && cursor.childCount < 1)
        {
            Instantiate(inventorySlots[currentSlot].Slot.GetChild(0).gameObject, cursor);
            Destroy(inventorySlots[currentSlot].Slot.GetChild(0).gameObject);
            isCursorWithItem = true;
        }
        else if (inventorySlots[currentSlot].Slot.childCount < 1 && cursor.childCount > 0)
        {
            Instantiate(cursor.GetChild(0).gameObject, inventorySlots[currentSlot].Slot);
            Destroy(cursor.GetChild(0).gameObject);
            isCursorWithItem = false;
        }
        else if (inventorySlots[currentSlot].Slot.childCount > 0 && cursor.childCount > 0)
        {
            if (inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId
                == cursor.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId)
            {
                int amount = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
                int maxStack = cursor.GetChild(0).GetComponent<UI_ItemData>().ItemData.StackLimit;
                if (amount <= maxStack - amount)
                {
                    inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount 
                        += cursor.GetChild(0).GetComponent<UI_ItemData>().Amount;
                    inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                    Destroy(cursor.GetChild(0).gameObject);
                    isCursorWithItem = false;
                }

            }
        }
        CheckIfSlotIsFull();
    }
    public void ItemHandlerVisibility()
    {
        if (cursor.childCount > 0)
        {
            cursor.gameObject.SetActive(true);
        }
        else cursor.gameObject.SetActive(false);
    }

    public void DropCurrentItem()
    {
        if (inventorySlots[currentSlot].Slot.childCount > 0)
        {
            GameObject prefab = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.Prefab;
            GameObject instantiateObject  = Instantiate(prefab, Camera.main.transform.position, Quaternion.identity);
            instantiateObject.GetComponent<ItemObjectInGame>().Amount = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
            instantiateObject.GetComponent<ItemObjectInGame>().InstanceInInventory.GetComponent<UI_ItemData>().Amount = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
            Destroy(inventorySlots[currentSlot].Slot.GetChild(0).gameObject);
        }
        CheckIfSlotIsFull();
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