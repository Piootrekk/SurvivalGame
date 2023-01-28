using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    [SerializeField] List<HitBox> hitBoxes = new List<HitBox>();
}

[System.Serializable]
public class HitBox
{
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] float damageMultipler;
    public CapsuleCollider CapsuleCollider => capsuleCollider;
    public float DamageMultipler => damageMultipler;
}