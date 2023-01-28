using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Well : MonoBehaviour
{
    [SerializeField] int maxLimit;
    [SerializeField] Transform prefabDrop;

    private int amount = 0;
    public int Amount { get => amount; set => amount = value; }
    public Transform PrefabDrop => prefabDrop;
    public void Start()
    {
        StartCoroutine(IncrementValue());
    }
    public IEnumerator IncrementValue()
    {
        while (amount <= 5)
        {
            amount++;
            yield return new WaitForSeconds(600);
        }
    }
}
