using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;

public class UI_InventoryManager : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private Transform inventorySlotHolder;
    [SerializeField] private Transform inventoryHotBarSlotHolder;
    [SerializeField] private List<UI_InventorySlot> inventorySlots;
    [SerializeField] private int currentSlot;

    [Header("Cursor - clicked item in inv")]
    [SerializeField] private Vector2 offset;
    [SerializeField] private Transform cursor;
    [SerializeField] bool isCursorWithItem;    
    
    [Header("Test")]
    [SerializeField] List<AllItemsInInventory> allItemsInInventory;

    private float timer = 0f;
    private float delay = 0.1f;
    private bool isExecuting = false;
    private GameObject inventory;

    public int CurrentSlot { get => currentSlot; set => currentSlot = value; }
    public bool IsCursorWithItem => isCursorWithItem;

    public List<AllItemsInInventory> AllItemsInInventory => allItemsInInventory;

    public List<UI_InventorySlot> InventorySlots => inventorySlots;

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
        InspectAllItemInInventory();
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
            CheckIfSlotIsFull();
            if(!(slot.Slot.childCount >= 1))
            {
                var prefabInstantiate = Instantiate(item, slot.Slot);
                prefabInstantiate.GetComponent<UI_ItemData>().Amount = itemObject.GetComponent<ItemObjectInGame>().Amount;
                prefabInstantiate.GetComponent<UI_ItemData>().Durability = itemObject.GetComponent<ItemObjectInGame>().Durability;
                CheckIfSlotIsFull();
                slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                return true;
            }
        }
        Debug.Log("Full Inventory");
        CheckIfSlotIsFull();
        return false;
    }

    private void SetSlotsID()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
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
    private void InspectAllItemInInventory()
    {
        allItemsInInventory.Clear();
        foreach (UI_InventorySlot slot in inventorySlots)
        {
            if (slot.Slot.childCount != 0)
            {
                int ID = slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId;
                int AMOUNT = slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
                int INDEX = allItemsInInventory.FindIndex(item => ID == item.Id);
                if (INDEX > -1)
                {
                    allItemsInInventory[INDEX].Amount += AMOUNT;
                }
                else allItemsInInventory.Add(new AllItemsInInventory(ID, AMOUNT));
            }
        }
    }


    public void GetItemToHandler()
    {
        CheckIfSlotIsFull();
        // jeœli klikamy na przedmiot, nie trzymaj¹c nic
        if (inventorySlots[currentSlot].Slot.childCount > 0 && cursor.childCount < 1)
        {
            Instantiate(inventorySlots[currentSlot].Slot.GetChild(0).gameObject, cursor);
            Destroy(inventorySlots[currentSlot].Slot.GetChild(0).gameObject);
            isCursorWithItem = true;
        }
        // jeœli odk³adamy przedmiot
        else if (inventorySlots[currentSlot].Slot.childCount < 1 && cursor.childCount > 0)
        {
            Instantiate(cursor.GetChild(0).gameObject, inventorySlots[currentSlot].Slot);
            Destroy(cursor.GetChild(0).gameObject);
            isCursorWithItem = false;
            CheckIfSlotIsUsed();
            CheckIfSlotIsFull();
        }
        // Jeœli odk³adamy przedmiot do istniej¹cego ju¿ tego samego przedmiotu
        else if (inventorySlots[currentSlot].Slot.childCount > 0 && cursor.childCount > 0)
        {
            if (inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId
                == cursor.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId)
            {
                if (inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount +
                    cursor.GetChild(0).GetComponent<UI_ItemData>().Amount
                    <= inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.StackLimit)
                {
                    inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount
                        += cursor.GetChild(0).GetComponent<UI_ItemData>().Amount;
                    inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                    Destroy(cursor.GetChild(0).gameObject);
                    isCursorWithItem = false;
                }
                else Debug.Log("FULL");

            }
        }
        CheckIfSlotIsUsed();
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
        if (inventorySlots[currentSlot].Slot.childCount > 0 && !isExecuting)
        {
            isExecuting = true;
            GameObject prefab = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.Prefab;
            GameObject instantiateObject  = Instantiate(prefab, Camera.main.transform.position, Quaternion.identity);
            instantiateObject.GetComponent<ItemObjectInGame>().Amount = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
            instantiateObject.GetComponent<ItemObjectInGame>().Durability = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Durability;
            instantiateObject.GetComponent<ItemObjectInGame>().InstanceInInventory.GetComponent<UI_ItemData>().Amount = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
            Destroy(inventorySlots[currentSlot].Slot.GetChild(0).gameObject);
            timer = Time.time;
            CheckIfSlotIsUsed();
        }
        else if (Time.time - timer > delay)
        {
            isExecuting = false;
        }
        CheckIfSlotIsFull();
    }


    public void CraftItem(CraftData craft)
    {
        var firstEmptySlot = inventorySlots.FirstOrDefault(x => x.IsFull == false);
        if (firstEmptySlot == null) return;
        int AMOUNT;
        foreach (var _craft in craft.Craft)
        {
            AMOUNT = _craft.Amount;
            foreach(UI_InventorySlot slot in inventorySlots)
            {
                if (slot.Slot.childCount != 0 && AMOUNT > 0)
                {
                    if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId == _craft.ItemData.ItemId)
                    {
                        if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount >= AMOUNT)
                        {
                            slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount -= AMOUNT;
                            AMOUNT = 0;
                        }
                        if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount < AMOUNT)
                        {
                            AMOUNT -= slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
                            slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount = 0;
                        }
                        CleanSlot(slot);
                        slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                    }
                }
               
            }
            
        }
        GenerateObjectFromCraft(craft);
        CheckIfSlotIsFull();
    }


    private void CleanSlot(UI_InventorySlot slot)
    {
        if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount == 0)
        {
            Destroy(slot.Slot.GetChild(0).gameObject);
            slot.IsFull = false;
        }
    }

    private void GenerateObjectFromCraft(CraftData craft)
    {
        // For slot with the same item
        foreach(UI_InventorySlot slot in inventorySlots)
        {
            if(slot.Slot.childCount != 0 && slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId == craft.Recive.GetComponent<UI_ItemData>().ItemData.ItemId)
            {
                if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount + craft.ReciveAmount <= slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.StackLimit)
                {
                    slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount += craft.ReciveAmount;
                    slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                    return;
                }
            }   
        }
        // For new slot
        var firstEmptySlot = inventorySlots.FirstOrDefault(x => x.IsFull == false);
        if (firstEmptySlot == null)  return;
        else Instantiate(craft.Recive, firstEmptySlot.Slot);
    }

    public void CheckIfSlotIsUsed()
    {
        IActiveSlot iActiveSlot = GameObject.FindObjectOfType<HotBarSlots>();
        iActiveSlot?.ActivateHotBarKeys();
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


[System.Serializable]
public class AllItemsInInventory
{
    [SerializeField] private int id;
    [SerializeField] private int amount;

    public int Id { get => id; set => id = value; }
    public int Amount { get => amount; set => amount = value; }
    public AllItemsInInventory(int id, int amount)
    {
        this.Id = id;
        this.Amount = amount;
    }
}