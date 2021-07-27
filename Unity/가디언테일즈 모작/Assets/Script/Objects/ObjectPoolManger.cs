using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManger : Singletone<ObjectPoolManger>
{
    [SerializeField] private GameObject dustPrefab;
    [SerializeField] private GameObject DestroyEffectPrefab;

    private Stack<GameObject> dustList;
    private Stack<GameObject> DestroyEffectList;
    // Start is called before the first frame update
    void Start()
    {
        dustList = new Stack<GameObject>();
        DestroyEffectList = new Stack<GameObject>();
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

    public void StorageDestroyEffect(GameObject obj)
    {
        DestroyEffectList.Push(obj);
    }

    public GameObject GenerateDestroyEffect()
    {
        GameObject obj;
        try
        {
            obj = DestroyEffectList.Pop();
        }
        catch
        {
            obj = Instantiate(DestroyEffectPrefab);
        }


        return obj;
    }
}
