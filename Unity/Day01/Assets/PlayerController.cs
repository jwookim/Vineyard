using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator animator;

    [SerializeField]
    Transform groundCheck;

    public bool isGrounded;
    public int JumpCount;
    public float speed;
    public float radius = 0.2f;
    public LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        speed = 3f;
        JumpCount = 1;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        //충돌되면 true 안되면 false
        //Physics2D.OverlapCircle : 원에 겹치면
        //Physics2D.OverlapCircle(중심점, 반지름, 레이어)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, radius, layerMask);
        

        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        transform.position += new Vector3(horizontal * Time.deltaTime * speed, 0f, 0f);

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (isGrounded)
        {
            JumpCount = 0;
        }

        if (JumpCount < 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.AddForce(Vector2.up * 300f);
                animator.SetTrigger("Jump");
                JumpCount++;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            animator.SetTrigger("Attack");
        }
    }
}
