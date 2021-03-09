using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    const float spreadRange = 1f;

    GameObject Effect;
    [SerializeField] private bool onOff;
    public bool OnOff { get { return onOff; } private set {; } }

    [SerializeField] float timeLimit;

    float currentTime;

    private void Awake()
    {
        Effect = transform.Find("Fire").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
        if (onOff)
            Ignite();
        else
            Extinguish();
    }

    // Update is called once per frame
    void Update()
    {
        if (onOff)
            Burn();
    }


    public void Ignite() //��ȭ
    {
        onOff = true;
        currentTime = timeLimit;
        Effect.SetActive(true);
    }
    
    void Extinguish() // ��ȭ
    {
        onOff = false;
        Effect.SetActive(false);
    }

    void Burn()
    {
        Spread();

        if (timeLimit > 0f)
        {
            currentTime -= Time.deltaTime;
            Debug.Log(currentTime);
            if (currentTime <= 0f)
                Extinguish();
        }
    }

    void Spread() // ����
    {
        foreach(var tmp in Physics.OverlapSphere(transform.position, spreadRange))
        {
            if (tmp == GetComponent<Collider>())
                continue;
            switch (tmp.tag)
            {
                case "Fire":
                    tmp.GetComponent<Fire>().Ignite();
                    break;
            }
        }
    }
}
