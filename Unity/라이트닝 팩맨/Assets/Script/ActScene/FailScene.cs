using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailScene : MonoBehaviour
{
    GameObject Invaders;
    GameObject Lana;

    bool press;
    // Start is called before the first frame update
    void Start()
    {
        Invaders = GameObject.Find("Invaders");
        Lana = GameObject.Find("Lana");
        press = false;
        StartCoroutine(Act());
    }

    // Update is called once per frame
    void Update()
    {
        if (press)
            InputKey();
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            SceneManager.LoadScene("MenuScene");
    }

    IEnumerator Act()
    {
        float scale = 1f;
        while (true)
        {
            Invaders.transform.localScale = new Vector3(scale, 1f, 1f);
            Lana.transform.localScale = new Vector3(scale, 1f, 1f);

            bool lanaCry = false;
            bool laugh = false;
            while(Lana.transform.position.x * scale  < 2.5f)
            {
                if(!lanaCry && Lana.transform.position.x * scale >= -2f)
                {
                    lanaCry = true;

                    Lana.GetComponent<AudioSource>().Play();
                }

                if (!laugh && Invaders.transform.position.x * scale >= -2f)
                {
                    laugh = true;

                    for (int i = 0; i < Invaders.transform.childCount; i++)
                        Invaders.transform.GetChild(i).GetComponent<AudioSource>().PlayDelayed(0.2f * i);
                }


                if (Invaders.transform.position.x * scale >= -1f)
                    Lana.transform.position += Vector3.right * 0.5f * scale * Time.deltaTime;


                if(Invaders.transform.position.x * scale < 2.5f)
                    Invaders.transform.position += Vector3.right * scale * Time.deltaTime;
                yield return null;
            }

            Lana.transform.position = new Vector3(2.5f * scale, Lana.transform.position.y, Lana.transform.position.z);
            Invaders.transform.position = new Vector3(2.5f * scale, Invaders.transform.position.y, Invaders.transform.position.z);

            scale *= -1f;
            press = true;
        }
    }
}
