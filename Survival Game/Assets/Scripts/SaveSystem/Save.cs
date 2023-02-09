using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Save : MonoBehaviour
{
    public void SavePlayerData()
    {
        SAVE save = new SAVE();
        save.Player = new(
            transform.position.x,
            transform.position.y,
            transform.position.z,
            transform.rotation.x,
            transform.rotation.y,
            transform.rotation.z,
            StatsManager.Instance.Health.CurrentPoints,
            StatsManager.Instance.Hunger.CurrentPoints,
            StatsManager.Instance.Thirst.CurrentPoints,
            StatsManager.Instance.Sleep.CurrentPoints,
            SkillManager.Instance.PlayerExp,
            StatsManager.Instance.Health.MaxPoints,
            StatsManager.Instance.Hunger.MaxPoints,
            StatsManager.Instance.Thirst.MaxPoints
            );

        save.WorldBasic = new(
            PlayerPrefs.GetInt("SEED0"),
            PlayerPrefs.GetInt("SEED1"),
            PlayerPrefs.GetInt("SEED2"),
            PlayerPrefs.GetInt("SIZE0"),
            PlayerPrefs.GetInt("SIZE1")
            );

        List<ResourcesScript> resObject = FindObjectsOfType<ResourcesScript>().ToList();
        
        save.Enviroments = new();
        foreach (ResourcesScript res in resObject)
        {
            save.Enviroments.Add(new Enviroment(
                res.transform.position.x,
                res.transform.position.y,
                res.transform.position.z,
                res.transform.rotation.x,
                res.transform.rotation.y,
                res.transform.rotation.z,
                res.CurrentHP,
                res.gameObject.name
                ));
        }
        save.Inventory = new();
        UI_InventoryManager manager = FindObjectOfType<UI_InventoryManager>();
        for (int i = 0; i < manager.InventorySlots.Count; i++)
        {
            if (manager.InventorySlots[i].Slot.childCount > 0)
            {
                save.Inventory.Add(new Inventory(
                    manager.InventorySlots[i].Slot.GetChild(0).GetComponent<UI_ItemData>().ItemData.ItemId,
                    i,
                    manager.InventorySlots[i].Slot.GetChild(0).GetComponent<UI_ItemData>().Amount,
                    manager.InventorySlots[i].Slot.GetChild(0).GetComponent<UI_ItemData>().Durability
                    ));
            }
        }

        string json = JsonUtility.ToJson(save, true);
        File.WriteAllText(Application.persistentDataPath + "/SAVE.json", json);
        Debug.Log("Data saved");
    }
}
