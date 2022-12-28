using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    public void OnAttack()
    {
        Debug.Log("Atack");
        if (HotBarSlots.Instance.ItemInUse == null) return;
    }

}
