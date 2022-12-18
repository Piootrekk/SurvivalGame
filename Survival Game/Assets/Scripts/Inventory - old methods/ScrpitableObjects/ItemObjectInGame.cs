using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectInGame : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject instanceInInventory;

    public GameObject InstanceInInventory => instanceInInventory;

    public void OnInteract()
    {
        Destroy(this.gameObject);
    }


}
