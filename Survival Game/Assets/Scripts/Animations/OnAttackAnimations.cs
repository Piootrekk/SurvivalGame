using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackAnimations : MonoBehaviour, IAnimation
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Animate()
    {
        animator.SetTrigger("Mouse1");
    }

}




interface IAnimation
{
    void Animate();
}