using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseAbstractItem : ScriptableObject
{
    [SerializeField] private int itemId;
    [SerializeField] private string nameItem;
    [SerializeField] private string description;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private GameObject prefab;
    [SerializeField] private bool isStackable;
    [SerializeField] private int stackLimit;
    [SerializeField] private ItemType itemType;



    public void OnEnable()
    {
        itemSprite = Resources.Load<Sprite>("Sprites/DefaultItemSprite");
    }


}



public enum ItemType
{
    Undefined,
    Resource,
    Consunable,
    Equipable,
    Wearable
}
