using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectInGame : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject instanceInInventory;
    [SerializeField] private int amount = 1;

    public GameObject InstanceInInventory => instanceInInventory;
    public int Amount { get => amount; set => amount = value; } 

    public void OnInteract()
    {
        InstanceInInventory.GetComponent<UI_ItemData>().Amount = amount;
        Destroy(this.gameObject);
    }


}
