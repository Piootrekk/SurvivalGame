using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CrafingManager : MonoBehaviour
{
    [SerializeField] List<CraftData> crafts;
    [SerializeField] Transform craftButton;
    [SerializeField] Transform itemSpriteRecieved;
    [SerializeField] Transform itemSpriteNeeded;

    private void Awake()
    {
        ImplementButtons();
    }

    private void Start()
    {
        ImplementContent();
    }

    private void ImplementButtons()
    {
        foreach (var craft in crafts)
        {
            Instantiate(craftButton, transform.GetChild(0));
        }
    }
    private void ImplementContent()
    {
        if (transform.GetChild(0).childCount <= 0) return;
        int i = 0;
        foreach (Transform child in transform.GetChild(0))
        {
            var instanceRecieved = Instantiate(itemSpriteRecieved, child.GetChild(0));
            instanceRecieved.GetComponent<Image>().sprite = crafts[i].Recive.GetComponent<UI_ItemData>().ItemData.ItemSprite;
            foreach (Craft craft in crafts[i].Craft)
            {
                var instanceNeed = Instantiate(itemSpriteNeeded, child.GetChild(1));
                instanceNeed.GetChild(0).GetComponent<Image>().sprite = craft.ItemData.ItemSprite;
                instanceNeed.GetChild(1).GetComponent<TextMeshProUGUI>().text = craft.Amount.ToString();
            }
            i++;
        }
    }

}
