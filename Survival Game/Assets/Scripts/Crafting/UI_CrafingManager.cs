using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class UI_CrafingManager : MonoBehaviour
{
    [SerializeField] List<CraftData> crafts;
    [SerializeField] Transform craftButton;
    [SerializeField] Transform itemSpriteRecieved;
    [SerializeField] Transform itemSpriteNeeded;

    private UI_InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<UI_InventoryManager>();
        ImplementButtons();
    }

    private void Start()
    {
        ImplementContent();
    }
    private void Update()
    {
        CheckItemsForCrafting();
    }

    private void ImplementButtons()
    {
        foreach (var craft in crafts)
        {
            var button = Instantiate(craftButton, transform.GetChild(0));
            button.GetComponent<Button>().interactable = false;
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

    private void CheckItemsForCrafting()
    {
        for(int i = 0; i < crafts.Count; i++)
        {
            List<bool> CheckIf = new();
            foreach(var item in crafts[i].Craft)
            {
                if (!inventoryManager.AllItemsInInventory.Any()) return;
                var cos = inventoryManager.AllItemsInInventory.FirstOrDefault(s => s.Id == item.ItemData.ItemId);
                if (cos != null) CheckIf.Add(true);
                else CheckIf.Add(false);
            }
            if (CheckIf != null && CheckIf.All(x => x == true)) transform.GetChild(0).GetChild(i).GetComponent<Button>().interactable = true;
            else transform.GetChild(0).GetChild(i).GetComponent<Button>().interactable = false;
            CheckIf.Clear();
        }


    }

}
