using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangePunchedBool()
    {
        animator.SetBool("IsPunched", false);
    }

}