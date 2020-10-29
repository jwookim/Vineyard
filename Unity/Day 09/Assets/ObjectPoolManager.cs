using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    [SerializeField] private List<GameObject> enemies;

    private void Awake()
    {
        //list 초기화
        enemies = new List<GameObject>();
    }

    public GameObject GetEnmy()
    {
        //지금 풀에 있는 enemy들을 확인
        foreach (var enemy in enemies)
        {
            //해당되는 enemy가 비활성화라면
            if (enemy.activeSelf == false)
            {
                //해당되는 enemy 반환
                return enemy;
            }
        }

        //적을 만듬
        GameObject obj = Instantiate(enemyPrefab);
        //만들어서 비활성화 시켜줌
        obj.SetActive(false);
        //만든 적을 풀(enemies)에 담기
        enemies.Add(obj);

        //반환
        return obj;
    }

}
