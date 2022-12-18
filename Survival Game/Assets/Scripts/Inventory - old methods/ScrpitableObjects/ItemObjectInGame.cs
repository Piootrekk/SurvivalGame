using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectInGame : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject instanceInInventory;

    public GameObject InstanceInInventory => instanceInInventory;
    public int amount = 1;

    public void OnInteract()
    {
        InstanceInInventory.GetComponent<UI_ItemData>().Amount = amount;
        Destroy(this.gameObject);
    }


}
