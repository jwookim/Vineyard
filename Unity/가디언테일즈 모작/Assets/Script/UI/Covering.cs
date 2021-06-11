using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Covering : MonoBehaviour
{
    const float MaxHoleSize = 2500f;
    const float MinHoleSize = 0f;
    const float HoleSpeed = 2000f;

    GameObject Hole;
    GameObject DarkScreen;
    // Start is called before the first frame update
    void Awake()
    {
        Hole = transform.Find("Hole").gameObject;
        DarkScreen = transform.Find("Dark Screen").gameObject;
    }

    private void Start()
    {
        Hole.SetActive(false);
        DarkScreen.SetActive(false);
        gameObject.SetActive(false);
    }

    public Coroutine Activate()
    {
        Hole.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxHoleSize, MaxHoleSize);

        Hole.SetActive(true);
        DarkScreen.SetActive(true);

        gameObject.SetActive(true);

        return StartCoroutine(TurnOff());
    }


    IEnumerator TurnOff()
    {

        while(Hole.GetComponent<RectTransform>().sizeDelta.x > MinHoleSize)
        {
            Hole.GetComponent<RectTransform>().sizeDelta -= new Vector2(Time.deltaTime * HoleSpeed, Time.deltaTime * HoleSpeed);
            yield return null;
        }
    }

    IEnumerator TurnOn()
    {
        while (Hole.GetComponent<RectTransform>().sizeDelta.x < MaxHoleSize)
        {
            Hole.GetComponent<RectTransform>().sizeDelta += new Vector2(Time.deltaTime * HoleSpeed, Time.deltaTime * HoleSpeed);
            yield return null;
        }

        Hole.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxHoleSize, MaxHoleSize);

        Hole.SetActive(false);
        DarkScreen.SetActive(false);

        gameObject.SetActive(false);

    }
}
