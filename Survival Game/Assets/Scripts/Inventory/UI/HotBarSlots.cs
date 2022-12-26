using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarSlots : MonoBehaviour, IActiveSlot
{
    [SerializeField] Transform hotBarSlots;
    [SerializeField] Transform cameraEquip;

    private UI_InventoryManager inventoryManager;
    private InputManager inputManager;


    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
    }

    public void ActivateHotBarKeys()
    {
        if (cameraEquip.childCount > 0)
        {
            Destroy(cameraEquip.GetChild(0).gameObject);
        }
        foreach (Transform slot in hotBarSlots)
        {
            slot.GetComponent<ActiveSlot>().IsActive = false;
            slot.GetComponent<Image>().color = new Color(0.3098039f, 0.24313726f, 0.24313726f);

        }
        hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetComponent<Image>().color = new Color(0.18039216f, 0.145098f, 0.14901961f);
        hotBarSlots.GetChild((int)inputManager.HotBarKey -1).GetComponent<ActiveSlot>().IsActive = true;
        if (IsItemInSlotHotBar((int)inputManager.HotBarKey - 1) && IsItemEquipable((int)inputManager.HotBarKey - 1))
        {
            Instantiate(hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemInUse, cameraEquip);
        }
    }

    public void HotBarChange()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (inputManager.HotBarKey == (float)i)
            {
                //Color in HEX: $2E2626
                hotBarSlots.GetChild(i - 1).gameObject.GetComponent<Image>().color = new Color(0.18039216f, 0.145098f, 0.14901961f);
                hotBarSlots.GetChild(i - 1).gameObject.GetComponent<ActiveSlot>().IsActive = true;
                if (IsItemInSlotHotBar(i - 1) && IsItemEquipable(i - 1))
                {
                    Instantiate(hotBarSlots.GetChild(i - 1).GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemInUse, cameraEquip);
                }
            }
           
            else
            {
                // Color in HEX: #4F3E3E
                hotBarSlots.GetChild(i - 1).gameObject.GetComponent<Image>().color = new Color(0.3098039f, 0.24313726f, 0.24313726f);
                hotBarSlots.GetChild(i - 1).gameObject.GetComponent<ActiveSlot>().IsActive = false;
                if (cameraEquip.childCount > 0)
                {
                   Destroy(cameraEquip.GetChild(0).gameObject);
                }
            }
        }
    }

    private bool IsItemInSlotHotBar(int i)
    {
        if (hotBarSlots.GetChild(i).childCount > 0) return true;
        else return false;
    }
    
    private bool IsItemEquipable(int i)
    {
        if (hotBarSlots.GetChild(i).GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemType == ItemType.Equipable) return true;
        else return false;
    }
  
}


public interface IActiveSlot
{
    void ActivateHotBarKeys();
}