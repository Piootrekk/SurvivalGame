using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Slot : MonoBehaviour
{
    [SerializeField] private int id;
    


    private UI_InventoryManager inventoryManager;

    public int ID { get => id; set => id = value; }

    private void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<UI_InventoryManager>();
    }

    public void SetID()
    {
        inventoryManager.CurrentSlot = id;
    }
}
// Zrobi� Interfejs obs�uguj�cy wy��czenie ruszaniem myszki
public interface IInventoryManager
{
    public bool IsGUIOff { set; get; }
    public void SetGUIOff();
}
