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

        //other�� null�̸� �浹�Ȱ��� ����
        if (other != null)
        {
            if (other.name == "Hunter")
            {
                target = other.transform;
                animator.SetFloat("Speed", 0f);
            }
            else
            {
                //���� �� ���ӿ�����Ʈ�� Vector3�� ����� ���� ����
                Vector3 vector3 = transform.localScale;
                //����� ���� x���� * -1 ex) 1 -> -1, -1 -> 1
                vector3.x *= -1f;
                //�ٽ� ���� ���ӿ�����Ʈ�� ���� ���
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


