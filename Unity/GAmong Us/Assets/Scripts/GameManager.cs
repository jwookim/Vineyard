using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.Find("GameManager").GetComponent<GameManager>();
                DontDestroyOnLoad(GameObject.Find("GameManager"));
            }
            return instance;
        }
    }

    public string currentNickName;
    public string myPlayfabID;


    private float OriginBGMvolume;
    private float OriginSFXvolume;
    private float tmpBGMvolume;
    private float tmpSFXvolume;

    GameObject commonCanvas;
    GameObject MessageBox;
    GameObject OptionBox;

    // Start is called before the first frame update
    void Awake()
    {
        commonCanvas = transform.Find("CommonCanvas").gameObject;
        MessageBox = commonCanvas.transform.Find("MessageBox").gameObject;
        OptionBox = transform.Find("CommonCanvas").Find("OptionBox").gameObject;
    }

    private void Start()
    {
        LoadPlayerSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendSystemMessage(string message)
    {
        MessageBox.transform.Find("Text").GetComponent<Text>().text = message;
        MessageBox.SetActive(true);
    }

    public void CloseMessageBox() => MessageBox.SetActive(false);


    /// <summary>
    /// 옵션 관련
    /// </summary>
    /// 
    private void LoadPlayerSetting()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerSetting.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/PlayerSetting.json");

            var data = JsonUtility.FromJson<PlayerSetting>(json);

            OriginBGMvolume = data.BGMvolume;
            OriginSFXvolume = data.SFXvolume;
        }
        else
        {
            OriginBGMvolume = 0.5f;
            OriginSFXvolume = 0.5f;
        }
        ChangeBgmVolume(OriginBGMvolume);
        ChangeSfxVolume(OriginSFXvolume);
    }

    public void OpenOption()
    {
        OptionBox.SetActive(true);

        tmpBGMvolume = OriginBGMvolume;
        tmpSFXvolume = OriginSFXvolume;
        OptionBox.transform.Find("base").Find("BGM Slider").GetComponent<Slider>().value = tmpBGMvolume;
        OptionBox.transform.Find("base").Find("SFX Slider").GetComponent<Slider>().value = tmpSFXvolume;
    }

    public void ChangeBgmOption()
    {
        tmpBGMvolume = OptionBox.transform.Find("base").Find("BGM Slider").GetComponent<Slider>().value;
        ChangeBgmVolume(tmpBGMvolume);
    }

    public void ChangeSfxOption()
    {
        tmpSFXvolume = OptionBox.transform.Find("base").Find("SFX Slider").GetComponent<Slider>().value;
        ChangeSfxVolume(tmpSFXvolume);
    }


    public void ConfirmOption()
    {
        OriginBGMvolume = tmpBGMvolume;
        OriginSFXvolume = tmpSFXvolume;

        OptionBox.SetActive(false);

        PlayerSetting data = new PlayerSetting();
        data.BGMvolume = OriginBGMvolume;
        data.SFXvolume = OriginSFXvolume;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/PlayerSetting.json", json);
    }

    public void CancleOption()
    {
        OptionBox.transform.Find("base").Find("BGM Slider").GetComponent<Slider>().value = OriginBGMvolume;
        OptionBox.transform.Find("base").Find("SFX Slider").GetComponent<Slider>().value = OriginSFXvolume;

        OptionBox.SetActive(false);
    }


    private void ChangeBgmVolume(float value) => GetComponent<AudioSource>().volume = value;

    private void ChangeSfxVolume(float value)
    {
        var audioSources = FindObjectsOfType<AudioSource>();

        foreach(var audio in audioSources)
        {
            if (audio == GetComponent<AudioSource>())
                continue;

            audio.volume = value;
        }
    }

    public void ChangeBGM(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// 씬 로드
    /// </summary>
    public static void LoadThisScene()
    {
        if (GameObject.Find("GameManager") == null)
            SceneManager.LoadScene("GameManageScene", LoadSceneMode.Additive);
    }

}

[System.Serializable]
public class PlayerSetting
{
    public float BGMvolume;
    public float SFXvolume;
}