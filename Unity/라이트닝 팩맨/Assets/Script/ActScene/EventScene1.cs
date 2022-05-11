using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventScene1 : MonoBehaviour
{
    const float Updatex = -100f;
    GameObject Lana;
    GameObject Invaders;
    GameObject Guardian;

    const float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Lana = GameObject.Find("Lana");
        Invaders = GameObject.Find("Invaders");
        Guardian = GameObject.Find("Guardian");
        StartCoroutine(Act());
    }


    IEnumerator Act()
    {
        yield return new WaitForSeconds(0.5f);
        Lana.GetComponent<AudioSource>().PlayDelayed(0.5f);
        while(Invaders.transform.position.x > -11.8f + Updatex)
        {
            Lana.transform.position += Vector3.left * Time.deltaTime * speed;
            Invaders.transform.position += Vector3.left * Time.deltaTime * speed;
            yield return null;
        }

        Guardian.GetComponent<AudioSource>().Play();

        foreach (var invader in Invaders.GetComponentsInChildren<Animator>())
            invader.SetTrigger("Run");
        GameObject victim = Invaders.transform.Find("Assassin").gameObject;
        victim.GetComponent<AudioSource>().Play();
        victim.transform.parent = null;

        foreach (var invader in Invaders.GetComponentsInChildren<AudioSource>())
            invader.PlayDelayed(1f);

        Lana.transform.position = new Vector3(-12f + Updatex, 0f, 0f);
        Lana.transform.localScale = Vector3.one * 5f;
        Lana.GetComponent<Animator>().SetTrigger("Chase");

        Invaders.transform.localScale = Vector3.one;
        while (Lana.transform.position.x < 10f + Updatex)
        {
            Invaders.transform.position += Vector3.right * Time.deltaTime * speed;

            if (Invaders.transform.position.x > -3f + Updatex)
            {
                Guardian.transform.position += Vector3.right * Time.deltaTime * speed;
                Lana.transform.position += Vector3.right * Time.deltaTime * speed;
            }
            yield return null;
        }

        yield return new WaitForSeconds(2f);

        SceneManager.UnloadSceneAsync("EventScene1");
    }
}
