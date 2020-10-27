using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent meshAgent;
    public Transform target;

    float AttackDelay;
    private void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();

        AttackDelay = 2f;
    }

    private void Update()
    {



        if (target != null)
        {
            //NavMeshAgent.SetDestination(������(Vector3)) : ������ ���� �� ã��
            meshAgent.SetDestination(target.position);

            float distance = Vector3.Distance(transform.position, target.position);

            //2f ���� �۴ٸ�
            if (distance < 2f)
            {
                //�޽ÿ�����Ʈ ����
                meshAgent.isStopped = true;

                Attack();
            }
            else
            {
                //�޽ÿ�����Ʈ �۵�
                meshAgent.isStopped = false;
            }

        }


        float speed = meshAgent.desiredVelocity.magnitude;

        //�ִϸ��̼� ����
        animator.SetFloat("Speed", speed);


        AttackDelay += Time.deltaTime;
    }


    public void Attack()
    {
        if(AttackDelay >= 2f)
        {
            AttackDelay = 0f;

            animator.SetTrigger("Attack");
        }
    }
}
