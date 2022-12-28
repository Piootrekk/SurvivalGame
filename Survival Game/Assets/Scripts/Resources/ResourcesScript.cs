using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesScript : MonoBehaviour
{
    [SerializeField] int currentHP;
    [SerializeField] int startHP;
    [SerializeField] List<Drop> drops;
    [SerializeField] GameObject particlesDuringHit;

    private void Awake()
    {
        currentHP = startHP;
    }


}



[System.Serializable]
public class Drop
{
    [SerializeField] ItemData item;
    [SerializeField] int amount;

    public ItemData Item => item;
    public int Amount => amount;
}
