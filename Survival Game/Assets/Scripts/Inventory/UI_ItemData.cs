using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemData : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int amount = 1;

    private GameObject amountText;

    public int Amount { get => amount; set => amount = value; }
    public ItemData ItemData { get => itemData; set => itemData = value; }

    private void OnEnable()
    {
        gameObject.GetComponent<Image>().sprite = itemData.ItemSprite;
        UpdateTextAmount();
    }

    public void UpdateTextAmount()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();
    }


}
