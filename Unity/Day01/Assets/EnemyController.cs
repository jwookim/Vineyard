using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public float moveSpeed = 5f;
    Transform target;
    public Transform colliderCheck;
    public LayerMask colliderMask;

    float atkDelay;



    void Start()
    {
        animator = GetComponent<Animator>();
        target = null;
        atkDelay = 0f;
    }


    void Update()
    {

        Collider2D other = Physics2D.OverlapPoint(colliderCheck.position, colliderMask);

        //other이 null이면 충돌된것이 없음
        if (other != null)
        {
            if (other.name == "Hunter")
            {
                target = other.transform;
                animator.SetFloat("Speed", 0f);
            }
            else
            {
                //현재 이 게임오브젝트의 Vector3를 복사된 값을 받음
                Vector3 vector3 = transform.localScale;
                //복사된 값의 x값을 * -1 ex) 1 -> -1, -1 -> 1
                vector3.x *= -1f;
                //다시 현재 게임오브젝트에 값을 덮어씀
                transform.localScale = vector3;

                target = null;
            }


        }
        else
            target = null;

        if(target)
        {
            Attack();
        }
        else
        {
            transform.Translate(transform.localScale.x * Vector2.right * Time.deltaTime * moveSpeed);

            animator.SetFloat("Speed", 5f);
        }

    }


    void Attack()
    {
        atkDelay += Time.deltaTime;

        if(atkDelay > 3f)
        {
            atkDelay = 0f;

            animator.SetTrigger("Attack");
        }
    }
}


