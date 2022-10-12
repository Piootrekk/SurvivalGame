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
        TEST_HealthManage();
        ReducingStatistic(Statslist[1]);
        ReducingStatistic(Statslist[2]);
        ReducingStatistic(Statslist[3]);
        ReducingHeal();
    }

    private void GetAllChildren()
    {
        foreach (Transform stats in StatsPanel)
        {
            Statslist.Add(stats.GetComponent<PlayerStats>());
        }
    }

    private void TEST_HealthManage()
    {
        if (inputManager.Test == default) { return; }

        if (inputManager.Test == -1)
        {
            Statslist[0].TakePoints(10f);
            Debug.Log("Zabrano ¿ycie");
        }
        else if (inputManager.Test == 1)
        {
            Statslist[0].RenegeratePoints(10f);
            Debug.Log("Uleczono");

        }
    }

    private void ReducingStatistic(PlayerStats stat)
    {
        stat.TakePoints(stat.DecayRate * Time.deltaTime);
    }

    private void ReducingHeal()
    {
        float reduceMultiply = 0f;
        if (Statslist[1].CurrentPoints <= 0.01f) reduceMultiply += 2;
        if (Statslist[2].CurrentPoints <= 0.01f) reduceMultiply += 3;
        if (Statslist[2].CurrentPoints <= 0.01f) reduceMultiply += 2;
        Statslist[0].TakePoints(reduceMultiply * Time.deltaTime);
    }



}
