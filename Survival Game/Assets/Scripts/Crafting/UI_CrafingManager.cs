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

    [SerializeField] int currentCraftPanel;

    private UI_InventoryManager inventoryManager;
    private List<CraftData> defaultCrafts;
    private ICraftInUse iCraftiInUse;

    public int CurrentCraftPanel { get => currentCraftPanel; set => currentCraftPanel = value; }
    public List<CraftData> Crafts { get => crafts; set => crafts = value; }

    private void Awake()
    {
        iCraftiInUse = FindObjectOfType<Crafting>();
        inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<UI_InventoryManager>();
        defaultCrafts = new(crafts.ToArray());
        ImplementButtons();
    }

    private void Start()
    {
        SetCraftPanelID();
        ImplementContent();
    }
    private void Update()
    {
        CheckItemsForCrafting();
        if (!Enumerable.SequenceEqual(crafts, defaultCrafts))
        {
            CraftChecker();
        }
    }


    private void ImplementButtons()
    {
        foreach (var craft in crafts)
        {
            var button = Instantiate(craftButton, transform.GetChild(0));
            button.GetComponent<Button>().interactable = false;
        }
    }

    private void DestroyButtons()
    {
        foreach (Transform child in transform.GetChild(0))
        {
            Destroy(child.gameObject);
        }
    }

    private void SetCraftPanelID()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            if (!transform.GetChild(0).GetChild(i).GetComponent<UI_CraftingPanel>()) return;
            transform.GetChild(0).GetChild(i).GetComponent<UI_CraftingPanel>().ID = i;
            transform.GetChild(0).GetChild(i).GetComponent<UI_CraftingPanel>().Craft = crafts[i];
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
        for (int i = 0; i < crafts.Count; i++)
        {
            if (CheckCraftingForSingleRecipe(crafts[i]))
            {
                transform.GetChild(0).GetChild(i).GetComponent<Button>().interactable = true;
            }
            else transform.GetChild(0).GetChild(i).GetComponent<Button>().interactable = false;
        }
    }

    private bool CheckCraftingForSingleRecipe(CraftData data)
    {
        List<bool> CheckIfCorrect = new();
        foreach (var item in data.Craft)
        {
            if (!inventoryManager.AllItemsInInventory.Any()) return false;
            var allItems = inventoryManager.AllItemsInInventory.FirstOrDefault(s => s.Id == item.ItemData.ItemId);

            if (allItems != null)
            {
                bool CheckIfAmountCorrect = allItems.Amount >= item.Amount;
                if (CheckIfAmountCorrect) CheckIfCorrect.Add(true);
                else CheckIfCorrect.Add(false);
            }

            else CheckIfCorrect.Add(false);
        }
        if (CheckIfCorrect != null && CheckIfCorrect.All(x => x == true))
        {
            return true;
        }
        else return false;
    }

    private void OrderByInteractable()
    {
        if (transform.GetChild(0).childCount == 0) return;
        foreach(Transform button in transform.GetChild(0))
        {
            if (button.GetSiblingIndex() == 0 && button.GetComponent<Button>().interactable)
            {
                button.SetSiblingIndex(0);
            }
        }
    }

    private void OrderByInteractable(Transform button)
    {
        if (button.GetSiblingIndex() == 0) return;
        if (button.GetComponent<Button>().interactable)
        {
            button.SetAsFirstSibling();
        }
    }


    public void SetDefalutCrafts()
    {
        crafts = new List<CraftData>();
        crafts.AddRange(defaultCrafts);
        DestroyButtons();
        ImplementButtons();
        Invoke(nameof(SetCraftPanelID), 0.1f);
        Invoke(nameof(ImplementContent), 0.15f);
    }

    public void AddCrafts(List<CraftData> list)
    {
        crafts = new List<CraftData>();
        crafts.AddRange(list);
        DestroyButtons();
        ImplementButtons();
        Invoke(nameof(SetCraftPanelID), 0.1f);
        Invoke(nameof(ImplementContent), 0.15f);
    }

    private void CraftChecker()
    {
        if (!iCraftiInUse.CraftingInUse)
        {
            SetDefalutCrafts();
        }
    }

}
