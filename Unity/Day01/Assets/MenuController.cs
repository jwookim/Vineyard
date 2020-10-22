using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void GoNextScene()
    {
        //"Game"¾À ºÒ·¯¿À±â
        SceneManager.LoadScene("Game");
        //SceneManager.LoadScene(1);
    }
}
