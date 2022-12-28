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
    private GameObject itemInUse;
    private static HotBarSlots instance;

    public Transform CameraEquip { get => cameraEquip; set => cameraEquip = value; }
    public GameObject ItemInUse => itemInUse;
    public static HotBarSlots Instance => instance;

    public void Awake()
    {
        inputManager = GetComponent<InputManager>();
        instance = this;
    }

    public void ActivateHotBarKeys()
    {

        foreach (Transform slot in hotBarSlots)
        {
            slot.GetComponent<ActiveSlot>().IsActive = false;
            slot.GetComponent<Image>().color = new Color(0.3098039f, 0.24313726f, 0.24313726f);

        }
        hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetComponent<Image>().color = new Color(0.18039216f, 0.145098f, 0.14901961f);
        if (IsItemInSlotHotBar((int)inputManager.HotBarKey - 1) && IsItemEquipable((int)inputManager.HotBarKey - 1) && cameraEquip.childCount == 0)
        {
            itemInUse = Instantiate(hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemInUse, cameraEquip);
            hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetComponent<ActiveSlot>().IsActive = true;
        }
        else if (cameraEquip.childCount > 0) Destroy(cameraEquip.GetChild(0).gameObject);
    }

    public bool IsItemInSlotHotBar(int i)
    {
        if (hotBarSlots.GetChild(i).childCount > 0) return true;
        else return false;
    }

    public bool IsItemEquipable(int i)
    {
        if (hotBarSlots.GetChild(i).GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemType == ItemType.Equipable) return true;
        else return false;
    }
  
}


public interface IActiveSlot
{
    //public Transform CameraEquip { get; set; }
    void ActivateHotBarKeys();
}