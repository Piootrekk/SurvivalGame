using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsunableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private ConsunableItem item;

    public string GetInteractPrompt()
    {
        return string.Format($"PickUp {item}");
    }

    public void OnInteract()
    {
        Destroy(gameObject);  
    }
}
