using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesScript : MonoBehaviour, IAttack
{
    [SerializeField] int currentHP;
    [SerializeField] int startHP;
    [SerializeField] List<Drop> drops;
    [SerializeField] GameObject particlesDuringHit;

    private void Awake()
    {
        currentHP = startHP;
    }

    public void OnAction(int damage, Vector3 hitpoint, Vector3 normal)
    {
        currentHP -= damage;
        Destroy(Instantiate(particlesDuringHit, hitpoint, Quaternion.LookRotation(normal, Vector3.up)), 1);
        if (currentHP <= 0)
        {
            foreach (Drop drop in drops)
            {
                var item = Instantiate(drop.Item.Prefab, transform.position, Quaternion.identity);
                item.GetComponent<ItemObjectInGame>().Amount = drop.Amount;
            }
            Destroy(gameObject);
        }
    }
}

public interface IAttack
{
    void OnAction(int damage, Vector3 hitpoint, Vector3 normal);
}



[System.Serializable]
public class Drop
{
    [SerializeField] ItemData item;
    [SerializeField] int amount;

    public ItemData Item => item;
    public int Amount => amount;
}
