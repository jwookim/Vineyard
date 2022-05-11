using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.SceneManagement;

public class ClearScene : MonoBehaviour
{
    GameObject Camera;
    AudioSource Music;

    bool press;
    // Start is called before the first frame update
    void Start()
    {
        press = false;
        Camera = GameObject.Find("Main Camera");
        Music = GetComponent<AudioSource>();
        StartCoroutine(Act());
    }

    private void Update()
    {
        InputKey();
    }

    void InputKey()
    {
        if (!press)
            return;
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            returnMenu();
    }

    void returnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    IEnumerator Act()
    {
        GameObject Eva = GameObject.Find("Eva");
        GameObject Marvin = GameObject.Find("Marvin");
        Music.volume = 0.5f;
        Music.Play();
        float timer = Time.time;
        yield return new WaitForSeconds(2f);

        while (Camera.transform.position.z < -1.5f)
        {
            Camera.transform.position += Vector3.forward * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while(Camera.transform.rotation.eulerAngles.y <= 180)
        {
            Camera.transform.rotation *= Quaternion.Euler(Vector3.down * 60f * Time.deltaTime);
            Music.volume += 0.5f / 3f * Time.deltaTime;
            yield return null;
        }

        Camera.transform.rotation = Quaternion.identity;
        Music.volume = 1f;

        while (Time.time - timer < 8f)
            yield return null;

        GameObject.Find("Light1").transform.Find("Spot Light").gameObject.SetActive(true);
        GameObject.Find("Light2").transform.Find("Spot Light").gameObject.SetActive(true);


        while (Time.time - timer < 13f)
            yield return null;

        Eva.transform.GetChild(0).gameObject.SetActive(false);
        Eva.GetComponent<Animator>().SetTrigger("OpenEye");

        while (Time.time - timer < 14f)
            yield return null;

        Eva.GetComponent<Animator>().SetTrigger("Dance!");
        Marvin.GetComponent<Animator>().SetTrigger("Dance!");

        GameObject.Find("Canvas").transform.Find("Thanx").gameObject.SetActive(true);
        press = true;

        while(true)
        {
            yield return new WaitForSeconds(1f);
            Eva.transform.localScale = new Vector3(Eva.transform.localScale.x * -1f, Eva.transform.localScale.y, Eva.transform.localScale.z);
            Marvin.transform.localScale = new Vector3(Marvin.transform.localScale.x * -1f, Marvin.transform.localScale.y, Marvin.transform.localScale.z);
        }

    }
}
