using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Save : MonoBehaviour
{
    public void SavePlayerData()
    {
        Player playerData = new(
            transform.position.x,
            transform.position.y,
            transform.position.z,
            transform.rotation.x,
            transform.rotation.y,
            transform.rotation.z,
            StatsManager.Instance.Health.CurrentPoints,
            StatsManager.Instance.Hunger.CurrentPoints,
            StatsManager.Instance.Thirst.CurrentPoints,
            StatsManager.Instance.Sleep.CurrentPoints
            );
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(Application.persistentDataPath + "/player.json", json);
        Debug.Log("Data saved");
    }
}
