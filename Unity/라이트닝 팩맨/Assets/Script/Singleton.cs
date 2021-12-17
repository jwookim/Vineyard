using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone<T> : MonoBehaviour where T : Component
{
    private static T instance;

    static public T Instance
    {
        get
        {
            instance = FindObjectOfType<T>();

            if (instance == null)
            {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;

                instance = obj.AddComponent<T>();
            }

            return instance;
        }

    }
}
