using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal * Time.deltaTime * speed, vertical * Time.deltaTime * speed, 0f);

        animator.SetFloat("Speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
    }
}
