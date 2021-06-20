using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBar : MonoBehaviour
{
    GameObject HpBar;
    GameObject SpBar;

    public GameObject parent;

    const float distance = -0.75f;
    private void Awake()
    {
        HpBar = transform.Find("HpBar").gameObject;
        SpBar = transform.Find("SpBar").gameObject;
    }

    private void Start()
    {
    }


    public void Activate(float Hpguage, float Spguage)
    {
        gameObject.SetActive(true);

        HpUpdate(Hpguage);
        SpUpdate(Spguage);
    }


    private void Update()
    {
        if(gameObject.activeSelf)
        {
            GetComponent<RectTransform>().position = parent.transform.position + new Vector3(0f, 0f, distance);
        }
    }

    public void HpUpdate(float guage)
    {
        if(gameObject.activeSelf)
        {
            HpBar.GetComponent<Image>().fillAmount = guage;
        }
    }

    public void SpUpdate(float guage)
    {
        if (gameObject.activeSelf)
        {
            SpBar.GetComponent<Image>().fillAmount = guage;
        }
    }
}
