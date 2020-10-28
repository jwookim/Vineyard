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
            //NavMeshAgent.SetDestination(������(Vector3)) : ������ ���� �� ã��
            meshAgent.SetDestination(target.position);

            float distance = Vector3.Distance(transform.position, target.position);

            //2f ���� �۴ٸ�
            if (distance < 2f)
            {
                //�޽ÿ�����Ʈ ����
                meshAgent.isStopped = true;

                transform.LookAt(target.position);
                Attack();
            }
            else
            {
                //�޽ÿ�����Ʈ �۵�
                meshAgent.isStopped = false;
            }

        }
        else if (speed <= 0)
        {
            meshAgent.stoppingDistance = 0f;
            //stayTime�� 0.02f ����
            stayTime += Time.fixedDeltaTime;
            //���࿡ stayTime�� 3f �̻��̸�
            if (stayTime > 3f)
            {  //                           ���� ��ġ��   + x,z�� �������� ����(-10f ~ 10f)
                Vector3 destination = transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

                //�������� ���ο� ��������
                meshAgent.SetDestination(destination);
                //stayTime�� 0f
                stayTime = 0f;
            }

        }
        else
        {
            stayTime = 0f;
        }



        speed = meshAgent.desiredVelocity.magnitude;

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
