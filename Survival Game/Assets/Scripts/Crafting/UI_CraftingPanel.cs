using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CraftingPanel : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private CraftData craft;

    private UI_CrafingManager craftingManager;
    private UI_InventoryManager inventoryManager;

    public int ID { get => id; set => id = value; }
    public CraftData Craft { get => craft; set => craft = value; }

    public void Start()
    {
        craftingManager = GameObject.FindGameObjectWithTag("Crafting").GetComponent<UI_CrafingManager>();
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<UI_InventoryManager>();
    }

    public void RightClick()
    {
        craftingManager.CurrentCraftPanel = id;
        inventoryManager.CraftItem(craft);
    }
}
