using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float maxPickUpDistance;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] int damage;
    [SerializeField] Transform referenceToFace;
    private Ray ray;
    private int currentDamage;

    public int Damage => damage;
    public int CurrentDamage { get => currentDamage; set => currentDamage = value; }

    public void OnAttack()
    {
        ray = new Ray(referenceToFace.transform.position, referenceToFace.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, maxPickUpDistance, interactableLayer))
        {
            var item = hitInfo.collider.gameObject;
            if (item.CompareTag("Player"))
            {
                StatsManager.Instance.Health.TakePoints(currentDamage * DayNightCycleManager.Instance.DamageMultiplayer);
                Debug.Log(currentDamage);
            }
            else if (item.GetComponent<ResourcesScript>() != null)
            {
                item.GetComponent<ResourcesScript>().CurrentHP -= damage;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
}
