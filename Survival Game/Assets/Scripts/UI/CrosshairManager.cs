using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    [SerializeField] Transform crosshair;

    public Transform Crosshair { get => crosshair; set => crosshair = value; }
}
