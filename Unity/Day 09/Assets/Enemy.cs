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
    [SerializeField] private float stayTime;
    private void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();

        AttackDelay = 2f;
    }

    private void Update()
    {

        float speed = meshAgent.desiredVelocity.magnitude;

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

                transform.LookAt(target.position);
                Attack();
            }
            else
            {
                //메시에이전트 작동
                meshAgent.isStopped = false;
            }

        }
        else if (speed <= 0)
        {
            meshAgent.stoppingDistance = 0f;
            //stayTime에 0.02f 더함
            stayTime += Time.fixedDeltaTime;
            //만약에 stayTime이 3f 이상이면
            if (stayTime > 3f)
            {  //                           나의 위치값   + x,z의 랜덤값을 더함(-10f ~ 10f)
                Vector3 destination = transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

                //목적지는 새로운 목적지로
                meshAgent.SetDestination(destination);
                //stayTime은 0f
                stayTime = 0f;
            }

        }
        else
        {
            stayTime = 0f;
        }



        speed = meshAgent.desiredVelocity.magnitude;

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
