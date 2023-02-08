using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.AI.Navigation;

public class DynamicNavMesh : MonoBehaviour
{
    public bool CanCheck { get; set; } = false;

    private int objectCount;
    public void BuildMesh()
    {
        List<GameObject> objects = GameObject.FindObjectsOfType<GameObject>().ToList();
        objectCount = objects.Where(o => o.layer == LayerMask.NameToLayer("Resoursable") || o.layer == LayerMask.NameToLayer("Buildable")).Count();
        gameObject.GetComponent<NavMeshSurface>().BuildNavMesh();
        Debug.Log(objectCount);
    }

    private void FixedUpdate()
    {
        if (!CanCheck) return;
        List<GameObject> objects = FindObjectsOfType<GameObject>().ToList();
        int temp = objects.Where(o => o.layer == LayerMask.NameToLayer("Resoursable") || o.layer == LayerMask.NameToLayer("Buildable")).Count();
        if (temp != objectCount)
        {
            Debug.Log("Liczba wszystkich obiektów w scenie: " + objectCount);
            objectCount = temp;
            gameObject.GetComponent<NavMeshSurface>().Invoke("BuildNavMesh", 1);
        }
        
    }
}
//Source mesh Meat0_1 does not allow read access This will work in playmode in the editor but not in player