using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Animator animator;
    public Animator playerAnimator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            playerAnimator = FindObjectOfType<PlayerMovements>().animator;
            playerAnimator.SetBool("IsDucking", true);
        }
    }
}
