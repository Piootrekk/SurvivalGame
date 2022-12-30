using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] float maxPickUpDistance;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] int damage;

    private Ray ray;


    public void OnAttack()
    {
        if (HotBarSlots.Instance.ItemInUse == null) return;
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, maxPickUpDistance, interactableLayer))
        {
            var item = hitInfo.collider.gameObject;
            IAttack iAttack = item.GetComponent<IAttack>();
            AddCurrentAttackToDamage(iAttack);
            iAttack?.OnAction(damage, hitInfo.point, hitInfo.normal);
            HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ReduceDurability();
        }
    }

    public void AddCurrentAttackToDamage(IAttack iAttack)
    {
        if (iAttack?.AttackType == AttackType.StoneDamage)
        {
            damage = HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.StoneDamage;
        }
        else if (iAttack?.AttackType == AttackType.WoodDamage)
        {
            damage = HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.WoodDamage;
        }
        else if (iAttack?.AttackType == AttackType.EnemyDamage)
        {
            damage = HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.EnemyDamage;
        }
        else if (iAttack?.AttackType == AttackType.PlayerConstructionDamage)
        {
            damage = HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.PlayerConstructionDamage;
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
