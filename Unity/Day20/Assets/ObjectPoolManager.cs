using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    public List<Bullet> bullets;
    public List<Enemy> enemies;
    public List<Player> players;

    private void Awake()
    {
        //√ ±‚»≠
        bullets = new List<Bullet>();
        enemies = new List<Enemy>();
        players = new List<Player>();
    }


    public T GetObject<T>() where T : Component
    {
        foreach (var ob in Objects)
        {
            if (!ob.gameObject.activeSelf)
            {
                return ob;
            }
        }

        GameObject obj = Instantiate(Prefab);
        T newObject = obj.GetComponent<T>();
        obj.SetActive(false);
        Objects.Add(newObject);
        return newObject;
    }


}

