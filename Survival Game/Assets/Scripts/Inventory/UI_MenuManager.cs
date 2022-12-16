using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MenuManager : MonoBehaviour
{
    [SerializeField] GameObject inventoryMenu;

    private InputManager inputManager;
    void Awake()
    {
        inventoryMenu.SetActive(false);
        inputManager = GetComponent<InputManager>();
    }

    private void Update()
    {
        OnInventoryHandler();
    }

    private void OnInventoryHandler()
    {
        inventoryMenu.SetActive(inputManager.Inventory);
    }


}


// Zrobi� Interfejs obs�uguj�cy wy��czenie ruszaniem myszki
public interface IInventoryManager
{
    public bool IsGUIOff { set; get; }
    public void SetGUIOff();
}
