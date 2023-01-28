using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGeneration : MonoBehaviour
{
    [SerializeField] private float mapScale;
    [SerializeField] private List<TerrainType> terrainTypes;
    [SerializeField] private float heightMultiplier;
    [SerializeField] private AnimationCurve heightCurve;
    [SerializeField]  private List<Wave> waves;

    private NoiseMapGeneration noiseMapGeneration;
    private MeshRenderer tileRenderer;
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;

    public float MapScale => mapScale;
    public List<Wave> Waves { get => waves; set => waves = value; }

    private void Awake()
    {
        noiseMapGeneration = GetComponent<NoiseMapGeneration>();
        tileRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        GenerateTile();
    }

    private void GenerateTile()
    {
        Vector3[] meshVertices = meshFilter.mesh.vertices;
        int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        int tileWidth = tileDepth;
        float offsetX = -transform.position.x;
        float offsetZ = -transform.position.z;
        float[,] heightMap = noiseMapGeneration.GenerateNoiseMap(tileDepth, tileWidth, mapScale, offsetX, offsetZ, waves);
        Texture2D tileTexture = BuildTexture(heightMap);
        tileRenderer.material.mainTexture = tileTexture;
        UpdateMeshVertices(heightMap);
    }
    private Texture2D BuildTexture(float[,] heightMap) 
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);
        Color32[] colorMap = new Color32[tileDepth * tileWidth];
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                int colorIndex = zIndex * tileWidth + xIndex;
                float height = heightMap[zIndex, xIndex];
                TerrainType terrainType = ChooseTerrainType(height);
                colorMap[colorIndex] = terrainType.Color;
            }
        }
        Texture2D tileTexture = new(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels32(colorMap);
        tileTexture.Apply();
        return tileTexture;
    }

    private TerrainType ChooseTerrainType(float height)
    {
        foreach (var terrainType in terrainTypes)
        {
            if (height < terrainType.Height)
            {
                return terrainType;
            }
        }
        return terrainTypes[^1];
    }

    private void UpdateMeshVertices(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);
        Vector3[] meshVertices = meshFilter.mesh.vertices;
        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                float height = heightMap[zIndex, xIndex];
                Vector3 vertex = meshVertices[vertexIndex];
                meshVertices[vertexIndex] = new Vector3(vertex.x, heightCurve.Evaluate(height) * heightMultiplier, vertex.z);
                vertexIndex++;
            }
        }
        meshFilter.mesh.vertices = meshVertices;
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
        meshCollider.sharedMesh = meshFilter.mesh;
    }
}

[System.Serializable]
public class TerrainType
{
    [SerializeField] private string name;
    [SerializeField] private float height;
    [SerializeField] private Color color;

    public string Name => name;
    public float Height => height;
    public Color Color => color;

}
