using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    const float FadeOutSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Disapear();
    }

    void Init()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }

    void Disapear()
    {
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a -= FadeOutSpeed * Time.deltaTime;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

        if (tmp.a <= 0f)
        {
            Storage();
        }
    }

    void Storage()
    {
        ObjectPoolManger.Instance.StorageDust(gameObject);
    }
}
