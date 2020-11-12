using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Info
{
    Monster[] monsters;
}

[System.Serializable]
public class Monster
{
    string name;
    string prefabName;
    int lv;
    float hp;
    float mp;
}

public class JsonManager : MonoBehaviour
{
    TextAsset textAsset;

    private void Start()
    {

    }

}
