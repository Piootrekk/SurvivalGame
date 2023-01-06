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
        IInventoryManager inventoryManager = gameObject.GetComponent<IInventoryManager>();
        if (inputManager.Inventory || inputManager.ESC)
        {
            inventoryManager?.SetGUIForGame();
            
        }
        else
        {
            inventoryManager?.SetGUIForInventory();
        }

    }

}

public interface IInventoryManager
{
    public void SetGUIForInventory();
    public void SetGUIForGame();
}
