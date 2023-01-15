using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] int usage;
    [SerializeField] string methodName;
    public void OnInteract()
    {
        if (usage < 0)
        {
            Destroy(gameObject);
        }
        Invoke(methodName, 0f);
    }

    public void UseAsWell()
    {
        Well well = GetComponent<Well>();
        if (well.Amount <= 0) return;
        var prefab = Instantiate(well.PrefabDrop, transform.position, Quaternion.identity);
        prefab.GetComponent<ItemObjectInGame>().Amount = well.Amount;
        usage--;
        well.Amount = 0;
    }

    public void UseAsTent()
    {
        if (StatsManager.Instance.Sleep.CurrentPoints > StatsManager.Instance.Sleep.MaxPoints * 0.9f) return;
        Tent tent = GetComponent<Tent>();
        tent.Used = true;
        StatsManager.Instance.Sleep.AddPoints(50f);
        DayNightCycleManager.Instance.DayCycle += 0.4f;
        usage--;
    }

    public void UseAsCrafting()
    {
        Crafting crafting = GetComponent<Crafting>();
        crafting.OnInteract();
    }
}
