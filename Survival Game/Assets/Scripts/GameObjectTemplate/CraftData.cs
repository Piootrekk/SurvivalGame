using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Craft", menuName = "InventoryData/Craft", order = 1)]
public class CraftData : ScriptableObject
{
    [SerializeField] int idCraft;
    [SerializeField] List<Craft> craft;
    [SerializeField] GameObject recive;
    [SerializeField] int reciveAmount;

    public int IdCraft => idCraft;
    public List<Craft> Craft => craft;
    public GameObject Recive => recive;
    public int ReciveAmount => reciveAmount;
}

[System.Serializable]
public class Craft
{
    [SerializeField] ItemData itemData;
    [SerializeField] int amount;

    public ItemData ItemData { get => itemData; set => itemData = value; }
    public int Amount { get => amount; set => amount = value; }
}

