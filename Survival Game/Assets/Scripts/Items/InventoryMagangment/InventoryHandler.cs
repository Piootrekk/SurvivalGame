using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] private int inventorySize;

    [SerializeField] private InventorySystem inventorySystem;

    private static UnityAction<InventorySystem> dynamicInvDisplay;

    public InventorySystem InventorySystem => inventorySystem;
    public UnityAction<InventorySystem> DynamicInvDisplay => dynamicInvDisplay;

    public void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize);
    }


}
