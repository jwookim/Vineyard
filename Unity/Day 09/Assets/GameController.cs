using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= 5f)
        {
            int num = Random.Range(0, 7);
            Debug.Log(num);
            Instantiate(EnemyPrefab, transform.GetChild(num).transform.position, Quaternion.identity);
            spawnTime = 0f;
        }
    }
}
