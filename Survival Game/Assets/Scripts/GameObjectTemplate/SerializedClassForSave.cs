using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PostitionAndRotation
{
    [SerializeField] float posX;
    [SerializeField] float posY;
    [SerializeField] float posZ;
    [SerializeField] float rotX;
    [SerializeField] float rotY;
    [SerializeField] float rotZ;


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
    [SerializeField] float actualHealth;
    [SerializeField] float actualHunger;
    [SerializeField] float actualThirst;
    [SerializeField] float actualSleep;

    //State - limit
    [SerializeField] float maxHealth;
    [SerializeField] float maxHunger;
    [SerializeField] float maxThirst;
    [SerializeField] float maxSleep;

    public Player(float posX, float posY, float posZ, float rotX, float rotY, float rotZ, 
        float health,float hunger, float thirst, float sleep, float healthMax, float hungerMax, float thirstMax, float sleepMax)
        : base(posX, posY, posZ, rotX, rotY, rotZ)
    {
        this.actualHealth = health;
        this.actualHunger = hunger;
        this.actualThirst = thirst;
        this.actualSleep = sleep;
        this.maxHealth = healthMax;
        this.maxHunger = hungerMax;
        this.maxThirst = thirstMax;
        this.maxSleep = sleepMax;
    }
}


[System.Serializable]
public class BuildsAndResources: PostitionAndRotation
{
    public int currentHP;


    public BuildsAndResources(float posX, float posY, float posZ, float rotX, float rotY, float rotZ)
        : base(posX, posY, posZ, rotX, rotY, rotZ)
    {


    }

}

