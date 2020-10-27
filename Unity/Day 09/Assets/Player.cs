﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private Animator animator;

    float AttackDelay;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        AttackDelay = 2f;
    }

    private void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", vertical);


        //                  (0f, 0f, 1f)    * -1 ~ 1   *    0.02f            *  5f
        transform.Translate(Vector3.forward * vertical * Time.fixedDeltaTime * speed);


        transform.Rotate(Vector3.up * horizontal, Space.World);



        if(Input.GetKey(KeyCode.LeftControl))
        {
            Attack();
        }
        AttackDelay += Time.deltaTime;
    }

    public void Attack()
    {
        if (AttackDelay >= 2f)
        {
            AttackDelay = 0f;

            animator.SetTrigger("Attack");
        }
    }
}