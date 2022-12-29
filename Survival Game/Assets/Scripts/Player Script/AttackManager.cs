using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] float maxPickUpDistance;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] int damage;
    [SerializeField] AttackType typeofAttack;
    private Ray ray;

    private void Awake()
    {
        if(typeofAttack == AttackType.StoneDamage)
        {
            damage = HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.StoneDamage;
        }
        else if (typeofAttack == AttackType.WoodDamage)
        {
            damage = HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.WoodDamage;
        }
        else if (typeofAttack == AttackType.EnemyDamage)
        {
            damage = HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.EnemyDamage;
        }
        else if (typeofAttack == AttackType.PlayerConstructionDamage)
        {
            damage = HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.PlayerConstructionDamage;
        }
    }

    public void OnAttack()
    {
        if (HotBarSlots.Instance.ItemInUse == null) return;
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, maxPickUpDistance, interactableLayer))
        {
            var item = hitInfo.collider.gameObject;
            IAttack iAttack = item.GetComponent<IAttack>();
            iAttack?.OnAction(damage, hitInfo.point, hitInfo.normal);
            HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ReduceDurability();
        }
    }
}

public enum AttackType
{
    StoneDamage,
    EnemyDamage,
    WoodDamage,
    PlayerConstructionDamage
}
