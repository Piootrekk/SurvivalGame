using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int itemQuantity;

    public ItemData ItemData => itemData;
    public int ItemQuantity => itemQuantity;

    public InventorySlot(ItemData item, int quantity)
    {
        itemData = item;
        itemQuantity = quantity;
    }

    public InventorySlot()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        itemQuantity = 0;
    }

    public void AddToStack(int amount)
    {
        itemQuantity += amount;
    }

    public void RemoveFromStack(int amount)
    {
        itemQuantity -= amount;
    }

    public bool IsSpaceLeftInStack(int amountAdded)
    {
        if (itemQuantity + amountAdded <= itemData.StackLimit) return true;
        else return false;
    }

    public int AmountReamaingInStack()
    {
        return itemData.StackLimit - itemQuantity;
    }

}
