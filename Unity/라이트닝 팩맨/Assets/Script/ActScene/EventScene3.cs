using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventScene3 : MonoBehaviour
{
    const float updateX = -100f;
    GameObject Lana, Clara, Elixir, Clara_Emo;

    [SerializeField] AudioClip Clara_voice;
    // Start is called before the first frame update
    void Start()
    {
        Lana = GameObject.Find("Wheelchair Lana");
        Clara = GameObject.Find("Clara");
        Elixir = GameObject.Find("ActElixir");
        Clara_Emo = Clara.transform.Find("Emo").gameObject;
        StartCoroutine(Act());
    }


    IEnumerator Act()
    {
        yield return new WaitForSeconds(0.5f);

        Clara.GetComponent<Animator>().SetBool("Move", true);
        while(Clara.transform.position.x > 6f + updateX)
        {
            Clara.transform.position += Vector3.left * Time.deltaTime * 2.5f;
            yield return null;
        }

        Clara.GetComponent<Animator>().SetBool("Move", false);

        yield return new WaitForSeconds(0.3f);

        Clara.GetComponent<Animator>().SetTrigger("Find");
        Clara.GetComponent<AudioSource>().Play();


        yield return new WaitForSeconds(1.5f);

        Clara.GetComponent<Animator>().SetBool("Move", true);
        bool check = false;
        bool lanaAct = false;
        while (Lana.transform.position.x > -10f + updateX)
        {
            if (Clara.transform.position.x < Lana.transform.position.x)
                Clara.transform.position += Vector3.left * Time.deltaTime * 2.5f;
            else if(!check)
            {
                Clara.GetComponent<Animator>().SetBool("Move", false);
                Clara.GetComponent<Animator>().SetTrigger("Suprise");
                Clara_Emo.GetComponent<Animator>().SetBool("Suprise", true);
            }

            if(Clara.transform.position.x < 4f + updateX)
            {
                if(!lanaAct)
                {
                    lanaAct = true;
                    Lana.GetComponent<AudioSource>().Play();
                }

                Lana.transform.position += Vector3.left * Time.deltaTime * 20f;

                if(Lana.transform.position.x < 0.5f + updateX)
                {
                    Elixir.transform.position += Vector3.left * Time.deltaTime * 20f;
                }
            }

            yield return null;
        }

        yield return new WaitForSeconds(2f);
        Lana = GameObject.Find("Lana");
        Lana.GetComponent<Animator>().SetBool("Move", true);
        while (Lana.transform.position.x < -7f + updateX)
        {
            Lana.transform.position += Vector3.right * Time.deltaTime * 5f;
            yield return null;
        }

        Lana.GetComponent<Animator>().SetBool("Move", false);

        yield return new WaitForSeconds(0.5f);

        Lana.GetComponent<Animator>().SetBool("Victory", true);
        Lana.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(2f);

        Lana.GetComponent<Animator>().SetBool("Victory", false);

        yield return new WaitForSeconds(0.3f);
        Lana.GetComponent<Animator>().SetBool("Move", true);
        Lana.transform.localScale = new Vector3(Lana.transform.localScale.x * -1f, Lana.transform.localScale.y, Lana.transform.localScale.z);
        while (Lana.transform.position.x > -11f + updateX)
        {
            Lana.transform.position += Vector3.left * Time.deltaTime * 5f;
            yield return null;
        }


        yield return new WaitForSeconds(0.3f);

        Clara.GetComponent<Animator>().SetTrigger("Angry");
        Clara_Emo.GetComponent<Animator>().SetBool("Suprise", false);
        Clara.GetComponent<AudioSource>().Play();
        while (Clara.transform.position.y < 0.5f)
        {
            Clara.transform.position += Vector3.up * Time.deltaTime * 3f;
            yield return null;
        }

        while (Clara.transform.position.y > 0f)
        {
            Clara.transform.position += Vector3.down * Time.deltaTime * 3f;
            yield return null;
        }

        Clara.GetComponent<AudioSource>().Play();
        while (Clara.transform.position.y < 0.5f)
        {
            Clara.transform.position += Vector3.up * Time.deltaTime * 3f;
            yield return null;
        }

        while (Clara.transform.position.y > 0f)
        {
            Clara.transform.position += Vector3.down * Time.deltaTime * 3f;
            yield return null;
        }


        yield return new WaitForSeconds(0.5f);

        Clara.GetComponent<AudioSource>().clip = Clara_voice;
        Clara.GetComponent<AudioSource>().Play();

        Lana.GetComponent<Animator>().SetBool("Dash", true);
        Clara.transform.Find("DashSound").GetComponent<AudioSource>().Play();
        while (Clara.transform.position.x > -12f + updateX)
        {
            Clara.transform.position += Vector3.left * Time.deltaTime * 10f;
            yield return null;
        }


        yield return new WaitForSeconds(2f);

        SceneManager.UnloadSceneAsync("EventScene3");
    }
}
