using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class PostitionAndRotation
{
    [SerializeField] public float posX;
    [SerializeField] public float posY;
    [SerializeField] public float posZ;
    [SerializeField] public float rotX;
    [SerializeField] public  float rotY;
    [SerializeField] public  float rotZ;

    public PostitionAndRotation(float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
    {
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
        this.rotX = rotX;
        this.rotY = rotY;
        this.rotZ = rotZ;
    }
}

    [System.Serializable]
public class Player : PostitionAndRotation
{
    //Stats - actual state
    [SerializeField] public float actualHealth;
    [SerializeField] public float actualHunger;
    [SerializeField] public float actualThirst;
    [SerializeField] public float actualSleep;
    [SerializeField] public int currentXp;
    [SerializeField] public float maxHealth;
    [SerializeField] public float maxHunger;
    [SerializeField] public float maxThirst;



    public Player(float posX, float posY, float posZ, float rotX, float rotY, float rotZ, 
        float health,float hunger, float thirst, float sleep, int xp, float maxhealth, float maxhunger, float maxthirst)
        : base(posX, posY, posZ, rotX, rotY, rotZ)
    {
        this.actualHealth = health;
        this.actualHunger = hunger;
        this.actualThirst = thirst;
        this.actualSleep = sleep;
        this.currentXp = xp;
        this.maxHealth = maxhealth;
        this.maxHunger = maxhunger;
        this.maxThirst = maxthirst;
    }
}


[System.Serializable]
public class Enviroment: PostitionAndRotation
{
    [SerializeField] public int currentHP;
    [SerializeField] public string name;


    public Enviroment(float posX, float posY, float posZ, float rotX, float rotY, float rotZ, int hp, string name)
        : base(posX, posY, posZ, rotX, rotY, rotZ)
    {
        this.name = name;
        this.currentHP = hp;
    }
}

[System.Serializable]
public class WorldBasic
{
    [SerializeField] public int seed1;
    [SerializeField] public int seed2;
    [SerializeField] public int seed3;
    [SerializeField] public int width;
    [SerializeField] public int depth;

    public WorldBasic(int s1, int s2, int s3, int width, int depth)
    {
        this.seed1 = s1;
        this.seed2 = s2;
        this.seed3 = s3;
        this.width = width;
        this.depth = depth;
    }
}


[System.Serializable]
public class Inventory
{
    [SerializeField] public int id;
    [SerializeField] public int slotinInv;
    [SerializeField] public int amount;
    [SerializeField] public int durability;

    public Inventory(int id, int slot, int amount, int durability)
    {
        this.id = id;
        this.slotinInv = slot;
        this.amount = amount;
        this.durability = durability;
    }
}

[System.Serializable]
public class SAVE
{
    [SerializeField] WorldBasic worldBasic;
    [SerializeField] Player player;
    [SerializeField] List<Inventory> inventory;
    [SerializeField] List<Enviroment> enviroments;

    public WorldBasic WorldBasic { get => worldBasic; set => worldBasic = value;  }
    public Player Player { get => player; set => player = value; }
    public List<Inventory> Inventory { get => inventory; set => inventory = value; }
    public List<Enviroment> Enviroments { get => enviroments; set => enviroments = value; }


}