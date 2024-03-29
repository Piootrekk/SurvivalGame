using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectInGame : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject instanceInInventory;
    [SerializeField] private int amount = 1;
    [SerializeField] private int durability = 0;
    private UI_InventoryManager inventoryHandler;

    public GameObject InstanceInInventory => instanceInInventory;
    public int Durability { get => durability; set => durability = value; }
    public int Amount { get => amount; set => amount = value; }

    private void Start()
    {
        inventoryHandler = GameObject.Find("Inventory").GetComponent<UI_InventoryManager>();
    }

    public void OnInteract()
    {
            if (!inventoryHandler) { return; }
            if (inventoryHandler.ItemAdd(gameObject))
            {
                Destroy(this.gameObject);
            }      
    }
}
