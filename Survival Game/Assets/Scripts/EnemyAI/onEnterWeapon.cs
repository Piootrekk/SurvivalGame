using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onEnterWeapon : MonoBehaviour
{
    [SerializeField] EnemyAttack mainObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HitBox>() == null) mainObject.CurrentDamage = mainObject.Damage;
        else
        {
            mainObject.CurrentDamage = (int)(mainObject.Damage * other.GetComponent<HitBox>().Multiply);
        }
    }

}
