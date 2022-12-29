using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] float maxPickUpDistance;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] int damage;
    private Ray ray;

    private void Awake()
    {
        damage = HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.StoneDamage;
    }

    public void OnAttack()
    {
        Debug.Log("Atack");
        if (HotBarSlots.Instance.ItemInUse == null) return;
        else Debug.Log(HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ItemData.NameItem);
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, maxPickUpDistance, interactableLayer))
        {
            Debug.Log("Stone hit?");
            Debug.Log(hitInfo.ToString());
            var item = hitInfo.collider.gameObject;
            Debug.Log(item.name);
            IAttack iAttack = item.GetComponent<IAttack>();
            iAttack?.OnAction(damage, hitInfo.point, hitInfo.normal);
            HotBarSlots.Instance.ItemInUse.GetComponent<UI_ItemData>().ReduceDurability();
        }

    }

}
