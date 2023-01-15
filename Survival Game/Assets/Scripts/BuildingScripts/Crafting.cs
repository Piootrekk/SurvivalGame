using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour, ICraftInUse
{
    [SerializeField] List<CraftData> listCraftsInCrafting;

    private InputManager inputManager;
    public bool CraftingInUse { get; set; } = false;

    private void Awake()
    {
        inputManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InputManager>();
        
    }

    private void Update()
    {
        if (!inputManager.Inventory) CraftingInUse = false;

    }

    public void OnInteract()
    {
        inputManager.Inventory = !inputManager.Inventory;
        PlayerController.Instance.CanWalk = false;
        UI_CrafingManager.Instance.AddCrafts(listCraftsInCrafting);
        CraftingInUse = true;
    }

}

public interface ICraftInUse
{
    bool CraftingInUse { get; set; }
}
