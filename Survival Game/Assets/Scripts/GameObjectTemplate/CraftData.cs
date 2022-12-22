using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Craft", menuName = "InventoryData/Craft", order = 1)]
public class CraftData : ScriptableObject
{
    [SerializeField] int idCraft;
    [SerializeField] List<Craft> craft;
    [SerializeField] GameObject recive;

    public int IdCraft => idCraft;
    public List<Craft> Craft => craft;
    public GameObject Recive => recive;
}

[System.Serializable]
public class Craft
{
    [SerializeField] int id;
    [SerializeField] int amount;

    public int Id { get => id; set => id = value; }
    public int Amount { get => amount; set => amount = value; }
}

