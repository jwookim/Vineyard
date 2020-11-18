using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class Dialogue
{
    public NPC[] npc;
}


[Serializable]
public class NPC
{
    public int index;
    public string talk;
    public bool button;
    public int buttonCount;
}

public class JsonManager : MonoBehaviour
{
    public string fileName = "Dialogue.json";
    public Dialogue dialogue;

    private void Awake()
    {
        string path = Path.Combine(Application.streamingAssetsPath, fileName);

        string jsonString = File.ReadAllText(path);

        dialogue = JsonUtility.FromJson<Dialogue>(jsonString);
        
    }
}