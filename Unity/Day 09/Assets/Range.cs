using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [SerializeField] private Enemy enemyController;

    private void Awake()
    {
        //transform.parent : �θ� Transform
        enemyController = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //���࿡ �浹ü�� �̸��� "Player" ���
        if (other.name == "Player")
        {
            //enemyController�� �ִ� target�� �浹ü�� transform
            enemyController.target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //enemyController�� �ִ� target�� null�� �ƴ϶��
        if (enemyController.target != null)
        {
            //enemyController�� �ִ� target�� null
            enemyController.target = null;
        }
    }

}
