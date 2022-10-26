using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] ItemData item;
    [SerializeField] int amount;

    public ItemData ItemData => item;
    public int Amount => amount;
    

    public void OnInteract()
    {
        Destroy(this.gameObject);
    }


}
