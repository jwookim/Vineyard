using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    [SerializeField] private List<GameObject> enemies;

    private void Awake()
    {
        //list �ʱ�ȭ
        enemies = new List<GameObject>();
    }

    public GameObject GetEnmy()
    {
        //���� Ǯ�� �ִ� enemy���� Ȯ��
        foreach (var enemy in enemies)
        {
            //�ش�Ǵ� enemy�� ��Ȱ��ȭ���
            if (enemy.activeSelf == false)
            {
                //�ش�Ǵ� enemy ��ȯ
                return enemy;
            }
        }

        //���� ����
        GameObject obj = Instantiate(enemyPrefab);
        //���� ��Ȱ��ȭ ������
        obj.SetActive(false);
        //���� ���� Ǯ(enemies)�� ���
        enemies.Add(obj);

        //��ȯ
        return obj;
    }

}
