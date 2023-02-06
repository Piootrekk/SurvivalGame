using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    [SerializeField] List<AllHitBox> hitBoxes = new List<AllHitBox>();
}

[System.Serializable]
public class AllHitBox
{
    [SerializeField] CapsuleCollider capsuleCollider;
    [SerializeField] float damageMultipler;
    public CapsuleCollider CapsuleCollider => capsuleCollider;
    public float DamageMultipler => damageMultipler;
}