using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class jsonSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SaveJson()
    {
        MapData mapData = new MapData();

        mapData.x = 28;
        mapData.y = 31;
        mapData.playerPos = new jsonVector(13f, 7f, -0.5f);
        mapData.shipPos = new jsonVector(13.5f, 16f, -0.5f);


        GameObject parent = GameObject.Find("obstacle");
        GameObject data;
        ObjectData objectData;
        for(int i = 0; i < parent.transform.childCount; i++)
        {
            data = parent.transform.GetChild(i).gameObject;

            objectData = new ObjectData();
            objectData.name = data.name.Split(' ')[0];
            objectData.position = new jsonVector(data.transform.position.x, data.transform.position.y, data.transform.position.z);
            objectData.rotation = new jsonVector(data.transform.rotation.eulerAngles.x, data.transform.rotation.eulerAngles.y, data.transform.rotation.eulerAngles.z);
            objectData.scale = new jsonVector(data.transform.localScale.x, data.transform.localScale.y, data.transform.localScale.z);

            mapData.obstacles.Add(objectData);
        }

        parent = GameObject.Find("object");
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            data = parent.transform.GetChild(i).gameObject;

            objectData = new ObjectData();
            objectData.name = data.name.Split(' ')[0];
            objectData.position = new jsonVector(data.transform.position.x, data.transform.position.y, data.transform.position.z);
            objectData.rotation = new jsonVector(data.transform.rotation.eulerAngles.x, data.transform.rotation.eulerAngles.y, data.transform.rotation.eulerAngles.z);
            objectData.scale = new jsonVector(data.transform.localScale.x, data.transform.localScale.y, data.transform.localScale.z);

            mapData.objects.Add(objectData);
        }

        string json = JsonUtility.ToJson(mapData);

        File.WriteAllText(Application.streamingAssetsPath + "/default_mapData.json", json);
    }
}
