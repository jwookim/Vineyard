using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventScene2 : MonoBehaviour
{
    const float updateX = -100f;

    GameObject Lana, Soldier, Magician, Assassin, Wheelchair;

    [SerializeField] AudioClip clang, jump, embarrass, victory, scream1, scream2, scream3, step, brake;
    // Start is called before the first frame update
    void Start()
    {
        Lana = GameObject.Find("Robot");
        Soldier = GameObject.Find("Soldier");
        Magician = GameObject.Find("Magician");
        Assassin = GameObject.Find("Assassin");
        Wheelchair = GameObject.Find("WheelChair");

        StartCoroutine(Actor());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Actor()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Invaders());

        Lana.GetComponent<AudioSource>().Play();
        Lana.GetComponent<Animator>().SetBool("Move", true);
        while (Lana.transform.position.x < -7f + updateX)
        {
            Lana.transform.position += Vector3.right * Time.deltaTime * 3f;
            yield return null;
        }

        Lana.GetComponent<AudioSource>().loop = false;
        Lana.GetComponent<Animator>().SetBool("Move", false);


        yield return new WaitForSeconds(2f);


        Lana.transform.localScale = new Vector3(Lana.transform.localScale.x * -1f, Lana.transform.localScale.y, Lana.transform.localScale.z);

        Lana.GetComponent<AudioSource>().loop = true;
        Lana.GetComponent<AudioSource>().Play();
        Lana.GetComponent<Animator>().SetBool("Move", true);
        while (Lana.transform.position.x > -17f + updateX)
        {
            Lana.transform.position += Vector3.left * Time.deltaTime * 3f;
            yield return null;
        }

        Lana.GetComponent<AudioSource>().loop = false;
        Lana.GetComponent<Animator>().SetBool("Move", false);


        Lana.transform.localScale = new Vector3(Lana.transform.localScale.x * -1f, Lana.transform.localScale.y, Lana.transform.localScale.z);

        yield return new WaitForSeconds(2f);

        Lana.GetComponent<AudioSource>().pitch = 1.3f;
        Lana.GetComponent<AudioSource>().loop = true;
        Lana.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.5f);
        
        
        yield return StartCoroutine(Invaders2());


        bool check1 = false;
        bool check2 = false;
        bool check3 = false;
        Lana.GetComponent<Animator>().SetBool("Dash", true);
        while (Lana.transform.position.x < 12f + updateX)
        {
            Lana.transform.position += Vector3.right * Time.deltaTime * 10f;

            if(Lana.transform.position.x >= updateX && !check1)
            {
                check1 = true;

                Magician.GetComponent<AudioSource>().clip = scream1;
                Magician.GetComponent<AudioSource>().Play();
                GetComponent<AudioSource>().Play();
                StartCoroutine(SwingMagician());
            }

            if (Lana.transform.position.x >= 1f + updateX && !check2)
            {
                check2 = true;

                Soldier.GetComponent<AudioSource>().clip = scream2;
                Soldier.GetComponent<AudioSource>().Play();
                GetComponent<AudioSource>().Play();
                StartCoroutine(SwingSoldier());
            }

            if (Lana.transform.position.x >= 5f + updateX && !check3)
            {
                check3 = true;

                Assassin.GetComponent<AudioSource>().clip = scream3;
                Assassin.GetComponent<AudioSource>().Play();
                GetComponent<AudioSource>().Play();
                StartCoroutine(SwingAssassin());
            }

            yield return null;
        }

        Lana.GetComponent<AudioSource>().Stop();
        Lana.GetComponent<AudioSource>().pitch = 1f;
        Lana.GetComponent<AudioSource>().loop = false;

        GetComponent<AudioSource>().clip = brake;
        GetComponent<AudioSource>().pitch = 1f;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1f);

        Lana.transform.localScale = new Vector3(Lana.transform.localScale.x * -1f, Lana.transform.localScale.y, Lana.transform.localScale.z);
        Lana.GetComponent<Animator>().SetBool("Dash", false);
        Lana.GetComponent<Animator>().SetBool("Move", true);
        Lana.GetComponent<AudioSource>().loop = true;
        Lana.GetComponent<AudioSource>().Play();
        while (Lana.transform.position.x > 3f + updateX)
        {
            Lana.transform.position += Vector3.left * Time.deltaTime * 3f;
            yield return null;
        }
        Lana.GetComponent<Animator>().SetBool("Move", false);
        Lana.GetComponent<AudioSource>().loop = false;

        yield return new WaitForSeconds(1f);

        Lana.GetComponent<Animator>().SetBool("Victory", true);
        Lana.GetComponent<AudioSource>().clip = victory;
        Lana.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1.5f);

        Lana.GetComponent<Animator>().SetBool("Victory", false);

        yield return new WaitForSeconds(0.2f);
        Lana.transform.localScale = new Vector3(Lana.transform.localScale.x * -1f, Lana.transform.localScale.y, Lana.transform.localScale.z);
        Lana.GetComponent<Animator>().SetLayerWeight(Lana.GetComponent<Animator>().GetLayerIndex("down"), 0.4f);
        Lana.GetComponent<Animator>().SetBool("Push", true);

        yield return new WaitForSeconds(0.3f);
        Lana.GetComponent<AudioSource>().clip = step;
        Lana.GetComponent<AudioSource>().loop = true;
        Lana.GetComponent<AudioSource>().Play();
        while (Lana.transform.position.x < 12f + updateX)
        {
            Lana.transform.position += Vector3.right * Time.deltaTime * 3f;
            Wheelchair.transform.position += Vector3.right * Time.deltaTime * 3f;

            yield return null;
        }

        Lana.GetComponent<AudioSource>().loop = false;


        yield return new WaitForSeconds(2f);

        SceneManager.UnloadSceneAsync("EventScene2");
    }

    IEnumerator Invaders()
    {
        yield return new WaitForSeconds(1f);
        Soldier.GetComponent<Animator>().SetBool("Gesture", true);
        Soldier.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1f);
        Assassin.GetComponent<Animator>().SetBool("Gesture", true);
        Assassin.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1.5f);

        Soldier.GetComponent<Animator>().SetBool("Gesture", false);

        yield return new WaitForSeconds(3f);

        Soldier.GetComponent<Animator>().SetBool("Repair", true);
        Soldier.GetComponent<AudioSource>().clip = clang;
        Soldier.GetComponent<AudioSource>().loop = true;
        Soldier.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(1f);
        Assassin.GetComponent<Animator>().SetBool("Gesture", false);
    }

    IEnumerator Invaders2()
    {
        Assassin.GetComponent<Animator>().SetBool("Saw", true);
        Assassin.GetComponent<AudioSource>().clip = jump;
        Assassin.GetComponent<AudioSource>().Play();

        while (Assassin.transform.position.y < 0.5f)
        {
            Assassin.transform.position += Vector3.up * Time.deltaTime * 3f;
            yield return null;
        }

        while (Assassin.transform.position.y > 0f)
        {
            Assassin.transform.position += Vector3.down * Time.deltaTime * 3f;
            yield return null;
        }


        yield return new WaitForSeconds(0.5f);

        Assassin.GetComponent<Animator>().SetTrigger("Embarrass");
        Assassin.transform.GetChild(0).gameObject.SetActive(true);
        Assassin.GetComponent<AudioSource>().clip = embarrass;
        Assassin.GetComponent<AudioSource>().Play();


        yield return new WaitForSeconds(0.5f);

        Soldier.GetComponent<Animator>().SetBool("Repair", false);
        Soldier.GetComponent<AudioSource>().Stop();
        Magician.transform.localScale = new Vector3(Magician.transform.localScale.x * -1f, Magician.transform.localScale.y, Magician.transform.localScale.z);


        yield return new WaitForSeconds(0.2f);

        Magician.GetComponent<Animator>().SetTrigger("Embarrass");
        Magician.GetComponent<AudioSource>().clip = embarrass;
        Magician.GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.5f);

        Soldier.transform.localScale = new Vector3(Soldier.transform.localScale.x * -1f, Soldier.transform.localScale.y, Soldier.transform.localScale.z);

        yield return new WaitForSeconds(0.5f);
        Soldier.GetComponent<Animator>().SetTrigger("Embarrass");
        Soldier.GetComponent<AudioSource>().loop = false;
        Soldier.GetComponent<AudioSource>().clip = embarrass;
        Soldier.GetComponent<AudioSource>().Play();
    }


    IEnumerator SwingMagician()
    {
        while (Magician.transform.position.y < 7.5f)
        {
            Magician.transform.position += new Vector3(-3f, 7.5f, 0f) * Time.deltaTime;
            Magician.transform.rotation *= Quaternion.Euler(new Vector3(0f, 0f, 1080f) * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator SwingSoldier()
    {
        while (Soldier.transform.position.y < 7.5f)
        {
            Soldier.transform.position += new Vector3(0f, 7.5f, 0f) * Time.deltaTime;
            Soldier.transform.rotation *= Quaternion.Euler(new Vector3(0f, 0f, -1080f) * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator SwingAssassin()
    {
        while (Assassin.transform.position.y < 7.5f)
        {
            Assassin.transform.position += new Vector3(5f, 7.5f, 0f) * Time.deltaTime;
            Assassin.transform.rotation *= Quaternion.Euler(new Vector3(0f, 0f, 1080f) * Time.deltaTime);
            yield return null;
        }
    }
}
