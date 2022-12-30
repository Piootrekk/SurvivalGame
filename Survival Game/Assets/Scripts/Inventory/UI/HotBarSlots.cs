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
    private GameObject _item;
    private static HotBarSlots instance;

    public Transform CameraEquip { get => cameraEquip; set => cameraEquip = value; }
    public GameObject ItemInUse { get => itemInUse; set => itemInUse = value; }
    public static HotBarSlots Instance => instance;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        instance = this;
    }
    private void Update()
    {
        CameraRemover();
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
            Debug.Log("Test1");
            itemInUse = hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetChild(0).gameObject;
            _item = Instantiate(itemInUse.GetComponent<UI_ItemData>().ItemData.ItemInUse, cameraEquip);
            hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetComponent<ActiveSlot>().IsActive = true;
        }
        else if (IsItemInSlotHotBar((int)inputManager.HotBarKey - 1) && IsItemEquipable((int)inputManager.HotBarKey - 1) && cameraEquip.childCount > 0)
        {
            Debug.Log("Test2");
            if(hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId != _item.GetComponent<ItemInUse>().Data.ItemId)
            {
                Destroy(cameraEquip.GetChild(0).gameObject);
                itemInUse = null;
                _item = null;

                Debug.Log("Test3");
                itemInUse = hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetChild(0).gameObject;
                _item = Instantiate(itemInUse.GetComponent<UI_ItemData>().ItemData.ItemInUse, cameraEquip);
                hotBarSlots.GetChild((int)inputManager.HotBarKey - 1).GetComponent<ActiveSlot>().IsActive = true;
            }
        }
    }

    public void DestroyCameraChild()
    {
        if (cameraEquip.childCount > 0)
        {
            Destroy(cameraEquip.GetChild(0).gameObject);
        } 
    }

    private void CameraRemover()
    {
        if (!IsItemInSlotHotBar((int)inputManager.HotBarKey - 1) && cameraEquip.childCount > 0)
        {
            Destroy(cameraEquip.GetChild(0).gameObject);
            itemInUse = null;
        }
        else if (IsItemInSlotHotBar((int)inputManager.HotBarKey - 1) && !IsItemEquipable((int)inputManager.HotBarKey - 1)
            && cameraEquip.childCount > 0)
        {
            Destroy(cameraEquip.GetChild(0).gameObject);
            itemInUse = null;
        }
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
    void ActivateHotBarKeys();
    void DestroyCameraChild();
}