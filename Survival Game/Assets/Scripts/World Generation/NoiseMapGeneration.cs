using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGeneration : MonoBehaviour
{
    public float[,] GenerateNoiseMap(int mapDepth, int mapWidth, float scale, float offsetX, float offsetZ, List<Wave> waves)
    {

        float[,] noiseMap = new float[mapDepth, mapWidth];
        for (int zIndex = 0; zIndex < mapDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < mapWidth; xIndex++)
            {
                float sampleX = (xIndex + offsetX) / scale;
                float sampleZ = (zIndex + offsetZ) / scale;

                float noise = 0f;
                float normalization = 0f;
                foreach (Wave wave in waves)
                {
                    noise += wave.Amplitude * Mathf.PerlinNoise(sampleX * wave.Frequency + wave.Seed, sampleZ * wave.Frequency + wave.Seed);
                    normalization += wave.Amplitude;
                    noiseMap[zIndex, xIndex] = noise;
                }
                noise /= normalization;
                noiseMap[zIndex, xIndex] = noise;
            }
        }
        return noiseMap;
    }
}


[System.Serializable]
public class Wave
{
    [SerializeField] private float seed;
    [SerializeField] private float frequency;
    [SerializeField] private float amplitude;

    public float Seed => seed;
    public float Frequency => frequency;
    public float Amplitude => amplitude;
}
