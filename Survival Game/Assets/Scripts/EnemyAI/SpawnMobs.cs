using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMobs : MonoBehaviour
{
    [SerializeField] Transform passiveMob;
    [SerializeField] Transform agresiveMob;
    [SerializeField] float maxHeight;
    [SerializeField] LayerMask mask;
    private Vector2 spawnPlace;
    private GameObject agrObject, passObject;

    public bool SpawnedSuccesfully { get; set; } = false;
    private void Start()
    {
        agrObject = new("Generated Agressive Mobs");
        passObject = new("Generated Passive Mobs");
    }

    private void FixedUpdate()
    {
        if (!SpawnedSuccesfully) return;
        if (agrObject.transform.childCount < 10)
        {
            SpawnAgresiveMob();
        }
        if (passObject.transform.childCount < 10)
        {
            SpawnPassiveMob();
        }
    }

    public void SpawnAgresiveMob()
    {
        spawnPlace = new(GetComponent<GenerateLevel>().SummarySizeX, GetComponent<GenerateLevel>().SummarySizeZ);
        float sampleX = Random.Range(0, spawnPlace.x);
        float sampleY = Random.Range(0, spawnPlace.y);
        Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity, mask))
        {
            var men = Instantiate(agresiveMob, hit.point, Quaternion.identity, agrObject.transform);
        }
    }

    public void SpawnPassiveMob()
    {
        spawnPlace = new(GetComponent<GenerateLevel>().SummarySizeX, GetComponent<GenerateLevel>().SummarySizeZ);
        float sampleX = Random.Range(0, spawnPlace.x);
        float sampleY = Random.Range(0, spawnPlace.y);
        Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity, mask))
        {
            var men = Instantiate(passiveMob, hit.point, Quaternion.identity, passObject.transform);

        }
    }


}