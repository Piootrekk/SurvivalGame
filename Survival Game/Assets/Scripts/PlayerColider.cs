using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerColider
{
    public float Height { get { return heigh; } set { heigh = value; } }
    public float Radius { get { return radius; } set { radius = value; } }
    public Vector3 Center { get { return speed; } set { speed = value; } }

    [SerializeField] private float heigh;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 speed;

}
