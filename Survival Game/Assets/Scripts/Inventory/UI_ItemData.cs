using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemData : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int amount;
    [SerializeField] private int durability;

    public int Amount { get => amount; set => amount = value; }
    public ItemData ItemData { get => itemData; set => itemData = value; }
    public int Durability { get => durability; set => durability = value; }

    private void OnEnable()
    {

        gameObject.GetComponent<Image>().sprite = itemData.ItemSprite;

    }
    private void Start()
    {
        UpdateTextAmount();
    }

    public void UpdateTextAmount()
    {
        if (itemData.ItemType == ItemType.Equipable)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        }
        else
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();
        }
    }

    public void ReduceDurability()
    {
        if(itemData.ItemType == ItemType.Equipable)
        {
            durability--;
            if(durability <= -1)
            {
                IActiveSlot iActiveSlot = GameObject.FindObjectOfType<HotBarSlots>();
                iActiveSlot?.DestroyCameraChild();
                Destroy(gameObject);
            }
        }
    }
}
