using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManger : Singletone<ObjectPoolManger>
{
    [SerializeField] private GameObject dustPrefab;

    private Stack<GameObject> dustList;
    // Start is called before the first frame update
    void Start()
    {
        dustList = new Stack<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StorageDust(GameObject obj)
    {
        dustList.Push(obj);
        obj.SetActive(false);
    }

    public void GenerateDust(Vector3 position)
    {
        GameObject obj;
        try
        {
            obj = dustList.Pop();
        }
        catch
        {
            obj = Instantiate(dustPrefab);
        }

        obj.transform.position = position;
        obj.SetActive(true);

    }
}
