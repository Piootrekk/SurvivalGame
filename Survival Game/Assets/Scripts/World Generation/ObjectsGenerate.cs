using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectsGenerate : MonoBehaviour
{
    [SerializeField] private List<ObjectOnTheMap> objects;
    [Space]
    [SerializeField] private LayerMask canSpawn;
    [SerializeField] private LayerMask cannotSpawn;
    [Space]
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 zRange;
    [Header("Prefab Variation Settings")]
    [SerializeField, Range(0, 1)] float rotateTowardsNormal;
    [SerializeField] Vector2 rotationRange;

    private SpawnMyMan spawnPlayer;

    public void Start()
    {
        Generate();
        spawnPlayer = GetComponent<SpawnMyMan>();
        spawnPlayer.GetSpawnRangeCoordinates();
        spawnPlayer.SpawnMyDude();
    }

    public void Generate()
    {
        GameObject emptyObject = new("Generated Resources");
        foreach (var prefab in objects)
        {
            for (int i = 0; i < prefab.Destiny; i++)
            {
                float sampleX = Random.Range(xRange.x, xRange.y);
                float sampleY = Random.Range(zRange.x, zRange.y);
                Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);
                if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity, canSpawn))
                    continue;
                if (hit.point.y < minHeight)
                    continue;

                GameObject instantiatedPrefab = Instantiate(prefab.Prefab, emptyObject.transform);
                if(instantiatedPrefab.GetComponent<MeshCollider>() != null)
                {
                    if (Physics.CheckBox(hit.point, instantiatedPrefab.GetComponent<MeshCollider>().bounds.size, instantiatedPrefab.transform.rotation, cannotSpawn))
                    {
                        Destroy(instantiatedPrefab);
                        Debug.Log("Object in the object");
                        continue;
                    }
                }
                instantiatedPrefab.transform.position = hit.point;
                if (instantiatedPrefab.GetComponent<BoxCollider>() != null) instantiatedPrefab.transform.position += new Vector3(0, 0.2f, 0);
                instantiatedPrefab.transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y), Space.Self);
                instantiatedPrefab.transform.rotation = Quaternion.Lerp(transform.rotation, transform.rotation * Quaternion.FromToRotation(instantiatedPrefab.transform.up, hit.normal), rotateTowardsNormal);
                
            }
        }

    }
}

[System.Serializable]
public class ObjectOnTheMap
{
    [SerializeField] GameObject prefab;
    [SerializeField] int destiny;
    public GameObject Prefab => prefab;
    public int Destiny => destiny;
}
