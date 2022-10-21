using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemData item;
    [SerializeField] int amount;
    

    public void OnInteract()
    {
        Destroy(this.gameObject);
        Debug.Log("Item pick up");
    }


}
