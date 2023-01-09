using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

public class StatsManager : MonoBehaviour
{
    [SerializeField] private Transform health;
    [SerializeField] private Transform hunger;
    [SerializeField] private Transform thirst;
    [SerializeField] private Transform sleep;

    public PlayerStats Health => health.GetComponent<PlayerStats>();
    public PlayerStats Hunger => hunger.GetComponent<PlayerStats>();
    public PlayerStats Thirst => thirst.GetComponent<PlayerStats>();
    public PlayerStats Sleep => sleep.GetComponent<PlayerStats>();


    private InputManager inputManager;
    private static StatsManager instance;
    
    public static StatsManager Instance => instance;
 

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        instance = this;
    }
    private void Update()
    {
        TEST_HealthManage();
        ReducingStatistic(Hunger);
        ReducingStatistic(Thirst);
        ReducingStatistic(Sleep);
        ReducingHeal();
    }


    private void TEST_HealthManage()
    {
        if (inputManager.Test == default) { return; }

        if (inputManager.Test == -1)
        {
            Health.TakePoints(10f);
            Debug.Log("Zabrano ¿ycie");
        }
        else if (inputManager.Test == 1)
        {
            Health.RenegeratePoints(10f);
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
        if (Hunger.CurrentPoints <= 0.01f) reduceMultiply += 2;
        if (Thirst.CurrentPoints <= 0.01f) reduceMultiply += 3;
        if (Sleep.CurrentPoints <= 0.01f) reduceMultiply += 2;
        Health.TakePoints(reduceMultiply * Time.deltaTime);
    }

}
