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
        foreach (Transform stats in StatsPanel)
        {
            Statslist.Add(stats.GetComponent<PlayerStats>());
        }
    }

    private void HealthManage()
    {
        if (inputManager.Test == default) { return; }

        if (inputManager.Test == -1)
        {
            Statslist[0].TakePoints(1f);
            Debug.Log("Zabrano ¿ycie");
        }
        else
        {
            Statslist[0].AddPoints(1f);
            Debug.Log("Uleczono");

        }
    }
}
