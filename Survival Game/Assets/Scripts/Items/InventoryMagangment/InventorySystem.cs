using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> InvSlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);
        for(int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }

    }

    public bool AddToInventory(Item item)
    {
        if (ContainsItem(item.ItemData, out List<InventorySlot> invSlot))
        {
            Debug.Log("Test1");
            foreach (InventorySlot slot in invSlot)
            {
                if(slot.IsSpaceLeftInStack(item.Amount))
                {
                    Debug.Log("Test2");
                    slot.AddToStack(item.Amount);
                    InvSlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }
        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            Debug.Log("Test3");
            freeSlot.UpdateInventorySlot(item.ItemData, item.Amount);
            InvSlotChanged?.Invoke(freeSlot);
            return true;
        }
        Debug.Log("Inventory full");
        return false;
    }

    public bool ContainsItem(ItemData itemAdded, out List<InventorySlot> invSlot)
    {
        invSlot = InventorySlots.Where(i => i.ItemData == itemAdded).ToList();
        if (invSlot == null)
        {
            return false;
        }
        else return true;
    }

    public bool HasFreeSlot(out InventorySlot slotLeft)
    {
        slotLeft = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        if (slotLeft == null)
        {
            return false;
        }
        else return true;
    }

}
