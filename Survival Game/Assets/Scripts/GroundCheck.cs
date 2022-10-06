using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    public bool IsGrounded { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null && (((1 << other.gameObject.layer) & ground) != 0))
        {
            IsGrounded = true;
            Debug.Log("Dziala");
        }

    }
    private void OnTriggerExit(Collider other)
    {
        IsGrounded = false;
        Debug.Log("Dziala exit");

    }
}
