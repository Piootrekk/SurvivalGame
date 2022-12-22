using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemData : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int amount;

    public int Amount { get => amount; set => amount = value; }
    public ItemData ItemData { get => itemData; set => itemData = value; }

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


}
