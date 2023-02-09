using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateLevelSAVE : MonoBehaviour
{
   
    [SerializeField] private int mapWidthInTiles;
    [SerializeField] private int mapDepthInTiles;
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform player;
    [SerializeField] List<GameObject> enviromentDataBase;
    public float SummarySizeX;
    public float SummarySizeZ;
    private SAVE save;

    void Awake()
    {
        LoadFromJson();
        InstantinateSeed();
        mapWidthInTiles = save.WorldBasic.width;
        mapDepthInTiles = save.WorldBasic.depth;
        GenerateMap();
    }

    private void Start()
    {
        GetComponent<DynamicNavMesh>().BuildMesh();
        GetComponent<DynamicNavMesh>().CanCheck = true;
        Instantiate(player);
        player.transform.position = new(save.Player.posX, save.Player.posY, save.Player.posZ);
        player.transform.rotation = Quaternion.Euler(save.Player.rotX, save.Player.rotY, save.Player.rotZ);
        player.GetComponent<StatsManager>().health.gameObject.GetComponent<PlayerStats>().CurrentPoints = save.Player.actualHealth;
        player.GetComponent<StatsManager>().hunger.gameObject.GetComponent<PlayerStats>().CurrentPoints = save.Player.actualHunger;
        player.GetComponent<StatsManager>().thirst.gameObject.GetComponent<PlayerStats>().CurrentPoints = save.Player.actualThirst;
        player.GetComponent<StatsManager>().health.gameObject.GetComponent<PlayerStats>().MaxPoints = save.Player.maxHealth;
        player.GetComponent<StatsManager>().hunger.gameObject.GetComponent<PlayerStats>().MaxPoints = save.Player.maxHunger;
        player.GetComponent<StatsManager>().thirst.gameObject.GetComponent<PlayerStats>().MaxPoints = save.Player.maxThirst;
        GetComponent<SpawnMyMan>().Player.GetComponent<StatsManager>().thirst.gameObject.GetComponent<PlayerStats>().MaxPoints = save.Player.maxThirst;
        player.GetChild(6).GetChild(5).GetChild(4).GetComponent<SkillManager>().PlayerExp = save.Player.currentXp;

        foreach (Enviroment env in save.Enviroments)
        {
            GameObject inst;
            string _name = env.name.Replace("(Clone)", "");
            foreach(var r in enviromentDataBase)
            {
                if(_name == r.name)
                {
                    inst = Instantiate(r);
                    inst.transform.position = new(env.posX, env.posY, env.posZ);
                    inst.GetComponent<ResourcesScript>().CurrentHP = env.currentHP;
                }
            }


        }
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
                Vector3 tilePosition = new(gameObject.transform.position.x + xTileIndex * tileWidth, gameObject.transform.position.y,
                    gameObject.transform.position.z + zTileIndex * tileDepth);
                var tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                tile.name = $"Chunk ({xTileIndex}:{zTileIndex})";
            }
        }
    }
    private void InstantinateSeed()
    {
        tilePrefab.GetComponent<TileGeneration>().Waves[0].Seed = save.WorldBasic.seed1;
        tilePrefab.GetComponent<TileGeneration>().Waves[1].Seed = save.WorldBasic.seed2;
        tilePrefab.GetComponent<TileGeneration>().Waves[2].Seed = save.WorldBasic.seed3;
    }

    private void LoadFromJson()
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/SAVE.json");
        save = JsonUtility.FromJson<SAVE>(json);
    }

}

