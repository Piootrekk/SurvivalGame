using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    [SerializeField] private int mapWidthInTiles;
    [SerializeField] private int mapDepthInTiles;
    [SerializeField] private GameObject tilePrefab;

    public float SummarySizeX;
    public float SummarySizeZ;

    void Awake()
    {
        GenerateMap();
    }
    private void GenerateMap()
    {
        Vector3 tileSize = tilePrefab.GetComponent<MeshRenderer>().bounds.size;
        int tileWidth = (int)tileSize.x;
        int tileDepth = (int)tileSize.z;
        SummarySizeX = mapWidthInTiles * tileSize.x;
        SummarySizeZ = mapDepthInTiles * tileSize.z;
        for (int xTileIndex = 0; xTileIndex < mapWidthInTiles; xTileIndex++)
        {
            for (int zTileIndex = 0; zTileIndex < mapDepthInTiles; zTileIndex++)
            { 
                Vector3 tilePosition = new(gameObject.transform.position.x + xTileIndex * tileWidth,gameObject.transform.position.y,
                    gameObject.transform.position.z + zTileIndex * tileDepth);
                var tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                tile.name = $"Chunk ({xTileIndex}:{zTileIndex})";
            }
        }
    }
}
