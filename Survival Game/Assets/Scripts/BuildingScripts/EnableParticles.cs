using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableParticles : MonoBehaviour
{
    [SerializeField] List<Transform> particles;
    private bool isEnable = false;

    private void Update()
    {
        if (isEnable && !gameObject.GetComponent<Crafting>().CraftingInUse) { Invoke(nameof(DisableParticle), 2f); isEnable = false; }
    }

    public void EnableParticle()
    {
        foreach (var particle in particles)
        {
            particle.gameObject.SetActive(true);
            isEnable = true;
        }
    }

    public void DisableParticle()
    {
        foreach (var particle in particles)
        {
            particle.gameObject.SetActive(false);
        }
    }
}
