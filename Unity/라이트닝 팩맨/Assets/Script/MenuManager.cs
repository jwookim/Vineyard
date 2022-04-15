using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    const float layoutSize = 18f;
    const float layoutSpeed = 10f;

    const float blinkTime = 0.5f;
    const float Pressblink = 0.1f;

    bool check;

    GameObject layout1, layout2;
    GameObject Text;

    Coroutine blinkText;
    // Start is called before the first frame update
    void Start()
    {
        layout1 = transform.Find("Layout").gameObject;
        layout2 = transform.Find("Layout2").gameObject;
        Text = GameObject.Find("Canvas").transform.Find("Text").gameObject;
        blinkText = StartCoroutine(BlinkText());
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        PressKey();
        MoveLayout();
        BlinkText();
    }


    void PressKey()
    {
        if (!check)
            return;

        if(Input.GetKeyDown(KeyCode.Tab))
            ScoreView();
        else if(Input.anyKeyDown)
        {
            StopCoroutine(blinkText);
            blinkText = StartCoroutine(PressBlinkText());

            transform.Find("PressSound").GetComponent<AudioSource>().Play();
            check = false;

            StartCoroutine(GameStart());
        }
    }

    void MoveLayout()
    {
        layout1.transform.position += new Vector3(0f, 1f, 0f) * Time.deltaTime * layoutSpeed;

        if (layout1.transform.position.y >= layoutSize)
            layout1.transform.position -= new Vector3(0f, layoutSize * 2f, 0f);

        layout2.transform.position += new Vector3(0f, 1f, 0f) * Time.deltaTime * layoutSpeed;

        if (layout2.transform.position.y >= layoutSize)
            layout2.transform.position -= new Vector3(0f, layoutSize * 2f, 0f);
    }

    void ScoreView()
    {
        GameObject board = GameObject.Find("Canvas").transform.Find("ScoreBoard").gameObject;
        if (!board.activeSelf)
        {
            string score = File.ReadAllText(Application.streamingAssetsPath + "/bestscore.json");
            board.transform.Find("Text").GetComponent<Text>().text = "Best Score : " + score;

            board.SetActive(true);
        }
        else
            board.SetActive(false);


    }

    IEnumerator GameStart()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("PlayScene");
        async.allowSceneActivation = false;

        yield return new WaitForSeconds(1.5f);

        GameObject panel = GameObject.Find("Canvas").transform.Find("Panel").gameObject;
        while (panel.GetComponent<Image>().color.a < 1f)
        {
            panel.GetComponent<Image>().color += new Color(0f, 0f, 0f, 1f) * Time.deltaTime;
            yield return null;
        }

        async.allowSceneActivation = true;
    }

    IEnumerator BlinkText()
    {
        float timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;

            if (timer >= blinkTime)
            {
                Text.SetActive(!Text.activeSelf);
                timer = 0f;
            }

            yield return null;
        }
    }

    IEnumerator PressBlinkText()
    {
        Text.SetActive(true);

        float timer = 0f;
        int count = 6;
        while (count > 0)
        {
            timer += Time.deltaTime;

            if (timer >= Pressblink)
            {
                Text.SetActive(!Text.activeSelf);
                timer = 0f;

                count--;
            }

            yield return null;
        }
    }
}
