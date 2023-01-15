using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour, ICraftInUse
{
    [SerializeField] List<CraftData> listCraftsInCrafting;
    [SerializeField] UI_CrafingManager craftingManager;

    //private UI_CrafingManager craftingManager;
    private InputManager inputManager;
    public bool CraftingInUse { get; set; } = false;

    private void Awake()
    {
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        
    }

    public void OnInteract()
    {
        inputManager.Inventory = !inputManager.Inventory;
        PlayerController.Instance.CanWalk = false;
        craftingManager.AddCrafts(listCraftsInCrafting);
        CraftingInUse = true;
    }

}

public interface ICraftInUse
{
    bool CraftingInUse { get; set; }
}
