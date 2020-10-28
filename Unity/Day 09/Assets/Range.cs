using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [SerializeField] private Enemy enemyController;

    private void Awake()
    {
        //transform.parent : 부모 Transform
        enemyController = transform.parent.GetComponent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //만약에 충돌체의 이름이 "Player" 라면
        if (other.name == "Player")
        {
            //enemyController에 있는 target에 충돌체의 transform
            enemyController.target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //enemyController에 있는 target이 null이 아니라면
        if (enemyController.target != null)
        {
            //enemyController에 있는 target은 null
            enemyController.target = null;
        }
    }

}
