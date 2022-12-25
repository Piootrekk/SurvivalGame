using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "InventoryData/Item")]
public class ItemData : ScriptableObject
{
    [Header("Basic")]
    [SerializeField] private int itemId;
    [SerializeField] private string nameItem;
    [SerializeField] private string description;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private GameObject prefab;
    [SerializeField] private bool isStackable;
    [SerializeField] private int stackLimit;
    [SerializeField] private ItemType itemType;
    [SerializeField] private GameObject itemInUse;
    [Header("Consunable")]
    [SerializeField] int staminaBonus;
    [SerializeField] int hungerBonus;
    [SerializeField] int thirstBonus;

    [Header("Attack")]
    [SerializeField] int enemyDamage;
    [SerializeField] int woodDamage;
    [SerializeField] int stoneDamage;

    [Header("Equipable")]
    [SerializeField] int durability;

    [Header("Buildable")]
    [SerializeField] int consturctionHP;

    [Header("Wearable")]
    [SerializeField] float damageAbsorbsion;

    public int ItemId => itemId;
    public string NameItem => nameItem;
    public string Description => description;
    public Sprite ItemSprite => itemSprite;
    public GameObject Prefab => prefab;
    public bool IsStackable => isStackable;
    public int StackLimit => stackLimit;
    public ItemType ItemType => itemType;
    public GameObject ItemInUse => itemInUse;
    public int StaminaBonus => staminaBonus;
    public int HungerBonus => hungerBonus;
    public int ThirstBonus => thirstBonus;
    public int EnemyDamage => enemyDamage;
    public int WoodDamage => woodDamage;
    public int StoneDamage => stoneDamage;
    public int Durability => durability;
    public int ConsturctionHP => consturctionHP;
    public float DamageAbsorbsion => damageAbsorbsion;




    public void OnEnable()
    {
        if (itemSprite == null)
        {
            itemSprite = Resources.Load<Sprite>("Sprites/DefaultItemSprite");
        }
        if (prefab == null)
        {
            prefab = Resources.Load<GameObject>("Prefabs/DefaultItemObject");
        }

    }


}
public enum ItemType
{
    Undefined,
    Resource,
    Consunable,
    Equipable,
    Wearable,
    Buildable
}

