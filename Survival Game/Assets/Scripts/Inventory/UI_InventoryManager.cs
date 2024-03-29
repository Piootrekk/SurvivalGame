using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class UI_InventoryManager : MonoBehaviour
{
    [Header("Inventory:")]
    [SerializeField] private Transform inventorySlotHolder;
    [SerializeField] private Transform inventoryHotBarSlotHolder;
    [SerializeField] private List<UI_InventorySlot> inventorySlots;
    [SerializeField] private int currentSlot;

    [Header("Cursor - selected item in inv:")]
    [SerializeField] private Vector2 offset;
    [SerializeField] private Transform cursor;
    [SerializeField] bool isCursorWithItem;    
    
    [Header("Buildable:")]
    [SerializeField] private Transform buildInUse;

    [Header("Stats:")]
    [SerializeField] private Transform itemName;
    [SerializeField] private List<Transform> stats;
    [SerializeField] private Transform desc;

    [Header("Test:")]
    [SerializeField] List<AllItemsInInventory> allItemsInInventory;

    private float timer = 0f;
    private float delay = 0.1f;
    private bool isExecuting = false;
    private GameObject inventory;

    public int CurrentSlot { get => currentSlot; set => currentSlot = value; }
    public bool IsCursorWithItem => isCursorWithItem;

    public List<AllItemsInInventory> AllItemsInInventory => allItemsInInventory;

    public List<UI_InventorySlot> InventorySlots => inventorySlots;

    private void Awake()
    {
        inventory = inventorySlotHolder.transform.parent.gameObject;
        SetSlotsForInventory();
        SetSlotsForHotBar();
        SetSlotsID();
        CheckIfSlotIsFull();
    }

    private void Update()
    {
        ItemHandler();
        ItemHandlerVisibility();
        InspectAllItemInInventory();
    }

    private void SetSlotsForInventory()
    {
        foreach(Transform slot in inventorySlotHolder)
        {
            if (slot == null) return;
            inventorySlots.Add(new(slot, false));
        }
    }

    private void SetSlotsForHotBar()
    {
        foreach(Transform slot in inventoryHotBarSlotHolder)
        {
            if (slot == null) return;
            inventorySlots.Add(new(slot, false));
        }
    }
    private void CheckIfSlotIsFull()
    {
        foreach(UI_InventorySlot slot in inventorySlots)
        {
            
            if(slot.Slot.childCount > 0)
            {
                slot.IsFull = true;
            }
            else slot.IsFull = false;
        }
        
    }

    private bool FindSameItemInIventory(UI_InventorySlot slot, GameObject itemObject)
    {
        var item = itemObject.GetComponent<ItemObjectInGame>().InstanceInInventory;
        if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId == item.GetComponent<UI_ItemData>().ItemData.ItemId)
        {
            int amount = slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
            int amountInObject = itemObject.GetComponent<ItemObjectInGame>().Amount;
            int maxStack = item.GetComponent<UI_ItemData>().ItemData.StackLimit;
            if (amount + amountInObject <= maxStack)
            {
                slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount
                    += amountInObject;
                slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                return true;
            }
            else return false;
        }
        return false;
    }

    public bool ItemAdd(GameObject itemObject)
    {
        CheckIfSlotIsFull();
        var item = itemObject.GetComponent<ItemObjectInGame>().InstanceInInventory;
        foreach (UI_InventorySlot slot in inventorySlots)
        {
            if (slot.IsFull && FindSameItemInIventory(slot, itemObject))
            {
                CheckIfSlotIsFull();
                return true;
            }
        }
        foreach (UI_InventorySlot slot in inventorySlots)
        {
            CheckIfSlotIsFull();
            if(!(slot.Slot.childCount >= 1))
            {
                var prefabInstantiate = Instantiate(item, slot.Slot);
                prefabInstantiate.GetComponent<UI_ItemData>().Amount = itemObject.GetComponent<ItemObjectInGame>().Amount;
                prefabInstantiate.GetComponent<UI_ItemData>().Durability = itemObject.GetComponent<ItemObjectInGame>().Durability;
                CheckIfSlotIsFull();
                slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                return true;
            }
        }
        Debug.Log("Full Inventory");
        CheckIfSlotIsFull();
        return false;
    }

    private void SetSlotsID()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (!inventorySlots[i].Slot.GetComponent<UI_Slot>()) return;
            inventorySlots[i].Slot.GetComponent<UI_Slot>().ID = i;
        }
    }

    private void ItemHandler()
    {
        if (!inventory.activeSelf) return;
        cursor.position = Mouse.current.position.ReadValue() + offset;
    }
    private void InspectAllItemInInventory()
    {
        allItemsInInventory.Clear();
        foreach (UI_InventorySlot slot in inventorySlots)
        {
            if (slot.Slot.childCount != 0)
            {
                int ID = slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId;
                int AMOUNT = slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
                int INDEX = allItemsInInventory.FindIndex(item => ID == item.Id);
                if (INDEX > -1)
                {
                    allItemsInInventory[INDEX].Amount += AMOUNT;
                }
                else allItemsInInventory.Add(new AllItemsInInventory(ID, AMOUNT));
            }
        }
    }

    public void GetItemToHandler()
    {
        CheckIfSlotIsFull();
        // je�li klikamy na przedmiot, nie trzymaj�c nic
        if (inventorySlots[currentSlot].Slot.childCount > 0 && cursor.childCount < 1)
        {
            Instantiate(inventorySlots[currentSlot].Slot.GetChild(0).gameObject, cursor);
            Destroy(inventorySlots[currentSlot].Slot.GetChild(0).gameObject);
            isCursorWithItem = true;
        }
        // je�li odk�adamy przedmiot
        else if (inventorySlots[currentSlot].Slot.childCount < 1 && cursor.childCount > 0)
        {
            Instantiate(cursor.GetChild(0).gameObject, inventorySlots[currentSlot].Slot);
            Destroy(cursor.GetChild(0).gameObject);
            isCursorWithItem = false;
            CheckIfSlotIsUsed();
            CheckIfSlotIsFull();
        }
        // Je�li odk�adamy przedmiot do istniej�cego ju� tego samego przedmiotu
        else if (inventorySlots[currentSlot].Slot.childCount > 0 && cursor.childCount > 0)
        {
            //Je�li ten sam przedmiot jest odk�adny do tego samego przedmiotu
            if (inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId
                == cursor.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId)
            {
                //Je�li ilo�� zsumowanych przedmiot�w nie przekracza limitu
                if (inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount +
                    cursor.GetChild(0).GetComponent<UI_ItemData>().Amount
                    <= inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.StackLimit)
                {
                    inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount
                        += cursor.GetChild(0).GetComponent<UI_ItemData>().Amount;
                    inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                    Destroy(cursor.GetChild(0).gameObject);
                    isCursorWithItem = false;
                }
                //Je�li ilo�� zsumowanych przedmiot�w PRZEKRACZA limit
                else
                {
                    int AMOUNT = cursor.GetChild(0).GetComponent<UI_ItemData>().Amount + inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount - inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.StackLimit;
                    inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.StackLimit;
                    inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                    Destroy(cursor.GetChild(0).gameObject);
                    var _cursor = Instantiate(inventorySlots[currentSlot].Slot.GetChild(0).gameObject, cursor);
                    _cursor.GetComponent<UI_ItemData>().Amount = AMOUNT;
                    isCursorWithItem = true;
                }
            }
            else
            {
                ReplaceCursorWithSlot(inventorySlots[currentSlot].Slot, cursor);
            }
        }
        CheckIfSlotIsUsed();
    }

    public void GetItemToHandlerRightClick()
    {
        if (inventorySlots[currentSlot].Slot.childCount > 0 && cursor.childCount < 1)
        {
            var _item = Instantiate(inventorySlots[currentSlot].Slot.GetChild(0).gameObject, cursor);
            _item.GetComponent<UI_ItemData>().Amount = 1;
            GetOneItem(inventorySlots[currentSlot]);
            isCursorWithItem = true;
        }
        else if (inventorySlots[currentSlot].Slot.childCount > 0 && cursor.childCount > 0)
        {
            if (inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId
                == cursor.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId)
            {
                cursor.GetChild(0).GetComponent<UI_ItemData>().Amount += 1;
                cursor.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                GetOneItem(inventorySlots[currentSlot]);
                isCursorWithItem = true;
            }
        }
    }


    public void ReplaceCursorWithSlot(Transform currentSlot, Transform cursor)
    {
        Instantiate(currentSlot.GetChild(0).gameObject, cursor);
        Instantiate(cursor.GetChild(0).gameObject, currentSlot);
        Destroy(currentSlot.GetChild(0).gameObject);
        Destroy(cursor.GetChild(0).gameObject);
        currentSlot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
    }

    public void ItemHandlerVisibility()
    {
        if (cursor.childCount > 0)
        {
            cursor.gameObject.SetActive(true);
        }
        else cursor.gameObject.SetActive(false);
    }

    public void DropCurrentItem()
    {
        if (inventorySlots[currentSlot].Slot.childCount > 0 && !isExecuting)
        {
            isExecuting = true;
            GameObject prefab = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.Prefab;
            GameObject instantiateObject  = Instantiate(prefab, Camera.main.transform.position, Quaternion.identity);
            instantiateObject.GetComponent<ItemObjectInGame>().Amount = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
            instantiateObject.GetComponent<ItemObjectInGame>().Durability = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Durability;
            instantiateObject.GetComponent<ItemObjectInGame>().InstanceInInventory.GetComponent<UI_ItemData>().Amount = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
            Destroy(inventorySlots[currentSlot].Slot.GetChild(0).gameObject);
            timer = Time.time;
            CheckIfSlotIsUsed();
        }
        else if (Time.time - timer > delay)
        {
            isExecuting = false;
        }
        CheckIfSlotIsFull();
    }

    public void CraftItem(CraftData craft)
    {
        var firstEmptySlot = inventorySlots.FirstOrDefault(x => x.IsFull == false);
        if (firstEmptySlot == null) return;
        int AMOUNT;
        foreach (var _craft in craft.Craft)
        {
            AMOUNT = _craft.Amount;
            foreach(UI_InventorySlot slot in inventorySlots)
            {
                if (slot.Slot.childCount != 0 && AMOUNT > 0)
                {
                    if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId == _craft.ItemData.ItemId)
                    {
                        if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount >= AMOUNT)
                        {
                            slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount -= AMOUNT;
                            AMOUNT = 0;
                        }
                        if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount < AMOUNT)
                        {
                            AMOUNT -= slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount;
                            slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount = 0;
                        }
                        CleanSlot(slot);
                        slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                    }
                }
               
            }
            
        }
        GenerateObjectFromCraft(craft);
        CheckIfSlotIsFull();
    }

    private void CleanSlot(UI_InventorySlot slot)
    {
        if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount == 0)
        {
            Destroy(slot.Slot.GetChild(0).gameObject);
            slot.IsFull = false;
        }
    }

    private void GenerateObjectFromCraft(CraftData craft)
    {
        // For slot with the same item
        foreach(UI_InventorySlot slot in inventorySlots)
        {
            if(slot.Slot.childCount != 0 && slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId == craft.Recive.GetComponent<UI_ItemData>().ItemData.ItemId)
            {
                if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount + craft.ReciveAmount <= slot.Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.StackLimit)
                {
                    slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount += craft.ReciveAmount;
                    slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
                    return;
                }
            }   
        }
        // For new slot
        var firstEmptySlot = inventorySlots.FirstOrDefault(x => x.IsFull == false);
        if (firstEmptySlot == null)  return;
        else Instantiate(craft.Recive, firstEmptySlot.Slot);
    }

    public void CheckIfSlotIsUsed()
    {
        IActiveSlot iActiveSlot = GameObject.FindObjectOfType<HotBarSlots>();
        iActiveSlot?.ActivateHotBarKeys();
    }

    public void UseCurrentItem()
    {
        if (inventorySlots[currentSlot].Slot.childCount == 0) return;
        if (inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemType == ItemType.Buildable)
        {

            Instantiate(inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemInUse, buildInUse);
            GetOneItem(inventorySlots[currentSlot]);
            if(buildInUse.childCount > 1)
            {
                for(int i = 1; i < buildInUse.childCount; i++)
                {
                    Destroy(buildInUse.GetChild(i).gameObject);
                }
            }
        }
        else if (inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemType == ItemType.Consunable)
        {
            var _data = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData;
            StatsManager.Instance.Health.AddPoints(_data.HealthBonus);
            StatsManager.Instance.Hunger.AddPoints(_data.HungerBonus);
            StatsManager.Instance.Thirst.AddPoints(_data.ThirstBonus);
            StatsManager.Instance.Sleep.AddPoints(_data.SleepBonus);
            GetOneItem(inventorySlots[currentSlot]);
        }
    }

    public void GetOneItem(UI_InventorySlot slot)
    {
        slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount -= 1;
        slot.Slot.GetChild(0).GetComponent<UI_ItemData>().UpdateTextAmount();
        DestroyIfZeroAmount();
    }

    private void DestroyIfZeroAmount()
    {
        foreach(UI_InventorySlot slot in inventorySlots)
        {
            if(slot.Slot.childCount != 0)
            {
                if (slot.Slot.GetChild(0).GetComponent<UI_ItemData>().Amount <= 0) Destroy(slot.Slot.GetChild(0).gameObject);
            }
        }
    }


    public void GetItemData()
    {
        if (inventorySlots[currentSlot].Slot.childCount == 0) return;
        var DATA = inventorySlots[currentSlot].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData;
        List<TextMeshProUGUI> statsList = stats.Select(x => x.GetComponent<TextMeshProUGUI>()).ToList();

        itemName.GetComponent<TextMeshProUGUI>().text = DATA.NameItem;
        desc.GetComponent<TextMeshProUGUI>().text = DATA.Description;
        if (DATA.SleepBonus != 0) { statsList[0].text = $"Sleep Bonus: {DATA.SleepBonus}"; statsList.RemoveAt(0); }
        if (DATA.HungerBonus != 0) { statsList[0].text = $"Hunger Bonus: {DATA.HungerBonus}"; statsList.RemoveAt(0); }
        if (DATA.ThirstBonus != 0) { statsList[0].text = $"Thirst Bonus: {DATA.ThirstBonus}"; statsList.RemoveAt(0); }
        if (DATA.HealthBonus != 0) { statsList[0].text = $"Health Bonus: {DATA.HealthBonus}"; statsList.RemoveAt(0); }
        if (DATA.EnemyDamage != 0) { statsList[0].text = $"Enemy damage: {DATA.EnemyDamage}"; statsList.RemoveAt(0); }
        if (DATA.WoodDamage != 0) { statsList[0].text = $"Wood damage: {DATA.WoodDamage}"; statsList.RemoveAt(0); }
        if (DATA.StoneDamage != 0) { statsList[0].text = $"Stone damage: {DATA.StoneDamage}"; statsList.RemoveAt(0); }
        if (DATA.PlayerConstructionDamage != 0) { statsList[0].text = $"Buildings damage: {DATA.PlayerConstructionDamage}"; statsList.RemoveAt(0); }
    }

    public void EmptyDataPanel()
    {
        itemName.GetComponent<TextMeshProUGUI>().text = "";
        desc.GetComponent<TextMeshProUGUI>().text = "";
        foreach (var stat in stats)
        {
            stat.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

}

[System.Serializable]
public class UI_InventorySlot
{
    [SerializeField] private Transform slot;
    [SerializeField] private bool isFull;

    public bool IsFull { get => isFull; set => isFull = value; }
    public Transform Slot { get => slot; set => slot = value; }


    public UI_InventorySlot(Transform slot, bool isFull)
    {
        this.slot = slot;
        this.isFull = isFull;
    }

}


[System.Serializable]
public class AllItemsInInventory
{
    [SerializeField] private int id;
    [SerializeField] private int amount;

    public int Id { get => id; set => id = value; }
    public int Amount { get => amount; set => amount = value; }
    public AllItemsInInventory(int id, int amount)
    {
        this.Id = id;
        this.Amount = amount;
    }
}
