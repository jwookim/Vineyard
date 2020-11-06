using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // »ç¿ë

public class JsonManager : MonoBehaviour
{
    private void Awake()
    {
        string path = Application.dataPath;

        string json = File.ReadAllText(path + "//Info.json");
        Debug.Log(json);
    }
}