using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager objectPoolManager;

    [SerializeField] float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        objectPoolManager = GetComponent<ObjectPoolManager>();

        spawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= 5f)
        {
            GameObject enemy = objectPoolManager.GetEnmy();

            if (enemy != null)
            {
                int num = Random.Range(0, 7);
                enemy.transform.position = transform.GetChild(num).transform.position;

                enemy.SetActive(true);
                
                spawnTime = 0f;

            }
        }
    }
}
