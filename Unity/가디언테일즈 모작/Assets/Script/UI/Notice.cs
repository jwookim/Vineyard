using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notice : MonoBehaviour
{
    string[] contents;
    int curNum;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            Close();
    }


    public void Modification(string[] str)
    {
        contents = str;
        curNum = 0;

        Activation();
    }

    private void Activation()
    {
        Debug.Log(contents[curNum]);
        transform.GetChild(0).GetComponent<Text>().text = contents[curNum];

        GameManager.Instance.PauseGame();
        gameObject.SetActive(true);
    }

    public void Close()
    {

        if (++curNum < contents.Length)
            Invoke("Activation", 0.5f);


        gameObject.SetActive(false);

        GameManager.Instance.InitSpeed();
    }
}
