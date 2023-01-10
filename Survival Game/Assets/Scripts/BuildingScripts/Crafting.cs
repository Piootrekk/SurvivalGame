using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    [SerializeField] float radious;
    [SerializeField] LayerMask layer;
    [SerializeField] UI_CrafingManager manager;

    [SerializeField] List<CraftData> listCraftsInCrafting;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        CheckIfPlayerIn();
    }

    private void CheckIfPlayerIn()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, radious, transform.forward, out hitInfo, radious, layer))
        {
            Debug.Log("Player in sphere");
           // manager.Crafts.AddRange(listCraftsInCrafting);
        }
    }

}
