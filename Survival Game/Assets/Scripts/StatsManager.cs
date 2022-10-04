using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private Transform StatsPanel;
    private List<PlayerStats> Statslist = new();
    private InputManager inputManager;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        GetAllChildren();
    }

    private void Update()
    {
        HealthManage();
    }

    private void GetAllChildren()
    {
        foreach (Transform obj in StatsPanel)
        {
            Statslist.Add(obj.GetComponent<PlayerStats>());
        }
    }

    private void HealthManage()
    {
        if (inputManager.Test == -1)
        {
            Statslist[0].TakePoints(5f);
            Debug.Log("Zadano dmg");
        }
        else if (inputManager.Test == 1)
        {
            Statslist[0].AddPoints(5f);
            Debug.Log("Uleczono");
        }
    }


}
