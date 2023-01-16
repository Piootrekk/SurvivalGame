using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMyMan : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float maxHeight;
    [SerializeField] LayerMask mask;
    private Vector2 spawnPlace;

    public void GetSpawnRangeCoordinates()
    {
        spawnPlace = new(GetComponent<GenerateLevel>().SummarySizeX, GetComponent<GenerateLevel>().SummarySizeZ);
    }
    public void SpawnMyDude()
    {
        float sampleX = Random.Range(spawnPlace.x / 4, spawnPlace.x / 2);
        float sampleY = Random.Range(spawnPlace.y / 4, spawnPlace.y / 2);
        Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);
        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity, mask))
        {
            var pleja = Instantiate(player, hit.point, Quaternion.identity);
        }
        else SpawnMyDude();
    }
}
