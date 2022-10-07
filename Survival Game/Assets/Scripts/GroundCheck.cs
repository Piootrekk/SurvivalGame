using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    public bool IsGrounded { get; private set; }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.gameObject.layer);
        Debug.Log(other.tag);
        //if (other != null && other.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
        //{
        //    IsGrounded = true;
        //    Debug.Log("Dziala");
        //}

    }
    private void OnTriggerExit(Collider other)
    {
        IsGrounded = false;
        Debug.Log($"{other.transform.gameObject.layer}, Dziala exit");
        Debug.Log(other.tag);
    }
}
