using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSlot : MonoBehaviour
{
    [SerializeField] private bool isActive;
    public bool IsActive { get => isActive; set => isActive = value; }
}
