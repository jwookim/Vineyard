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
            //NavMeshAgent.SetDestination(목적지(Vector3)) : 목적지 까지 길 찾기
            meshAgent.SetDestination(target.position);

            float distance = Vector3.Distance(transform.position, target.position);

            //2f 보다 작다면
            if (distance < 2f)
            {
                //메시에이전트 멈춤
                meshAgent.isStopped = true;

                Attack();
            }
            else
            {
                //메시에이전트 작동
                meshAgent.isStopped = false;
            }

        }


        float speed = meshAgent.desiredVelocity.magnitude;

        //애니메이션 적용
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
