using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] int usage;
    [SerializeField] string methodName;
    public void OnInteract()
    {
        UseAsWell();
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
        prefab.position += new Vector3(0f, 3f, 0f);
        usage--;
        well.Amount = 0;
    }

    public void UseAsTent()
    {

    }
}
