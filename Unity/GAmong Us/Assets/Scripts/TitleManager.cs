using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using PlayFab;
using PlayFab.ClientModels;
using System.Text.RegularExpressions;

public class TitleManager : MonoBehaviour
{

    const int IDlength = 4;

    InputField NameBox;
    GameObject RenameButton;
    PhotonManager networkManager;

    [SerializeField] AudioClip BGM;

    private void Awake()
    {
        GameManager.LoadThisScene();
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ChangeBGM(BGM);
        networkManager = GetComponent<PhotonManager>();
        NameBox = GameObject.Find("Canvas").transform.Find("NameBox").GetComponent<InputField>();
        RenameButton = GameObject.Find("Canvas").transform.Find("RenameButton").gameObject;

        Login();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ÇÃ·¹ÀÌÆÕ °ü·Ã ÇÔ¼ö
    /// </summary>

    public void Login()
    {
        string customid;
        if (!File.Exists(Application.persistentDataPath + "/customID.json"))
        {
            LoginWithNewID();
            return;
        }

        customid = File.ReadAllText(Application.persistentDataPath + "/customID.json");

        if (customid.Length != IDlength)
            LoginWithNewID();
        else
        {
            var request = new LoginWithCustomIDRequest { CustomId = customid, CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnPlayFabError);
        }
    }

    private void LoginWithNewID()
    {
        string newID = GetRandomId();
        File.WriteAllText(Application.persistentDataPath + "/customID.json", newID);
        var request = new LoginWithCustomIDRequest { CustomId = newID, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccessWithNewID, OnPlayFabError);
    }

    private string GetRandomId()
    {
        string newId = null;
        string randomStr = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        for (int i = 0; i < IDlength; i++)
            newId += randomStr[Random.Range(0, randomStr.Length)];

        return newId;
    }

    private void OnLoginSuccess(LoginResult loginResult)
    {
        Debug.Log("·Î±×ÀÎ ¼º°ø");
        GameManager.Instance.myPlayfabID = loginResult.PlayFabId;
        GetAccountInfo();
    }

    private void OnLoginSuccessWithNewID(LoginResult loginResult)
    {
        if (loginResult.NewlyCreated)
        {
            Debug.Log("°¡ÀÔ ¼º°ø");
        }
        else
        {
            Debug.Log("Áßº¹µÈ ¾ÆÀÌµð");
            LoginWithNewID();
        }
    }

    private void OnPlayFabError(PlayFabError error)
    {
        Debug.LogWarning("½ÇÆÐ");
        Debug.LogWarning(error.GenerateErrorReport());

        Application.Quit();
        /*Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());*/
    }


    private void GetAccountInfo()
    {
        var request = new GetAccountInfoRequest() { PlayFabId = GameManager.Instance.myPlayfabID };

        PlayFabClientAPI.GetAccountInfo(request, OnGetAccountInfoSuccess, OnPlayFabError);
    }

    private void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    {
        GameManager.Instance.currentNickName = result.AccountInfo.TitleInfo.DisplayName.Split('#')[0];
        NameBox.text = GameManager.Instance.currentNickName;

        HideRenameButton();
    }

    private void SetAccountInfo(string NickName)
    {
        string idChecker = Regex.Replace(NickName, @"[^0-9a-zA-z¤¡-¤¾¤¿-¤Ó°¡-ÆR\*]", "", RegexOptions.Singleline);
        if (NickName == "")
            GameManager.Instance.SendSystemMessage("´Ð³×ÀÓÀ» ÀÔ·ÂÇØÁÖ¼¼¿ä.");
        else if (!NickName.Equals(idChecker))
            GameManager.Instance.SendSystemMessage("´Ð³×ÀÓ¿¡ Æ¯¼ö¹®ÀÚ´Â Æ÷ÇÔµÉ ¼ö ¾ø½À´Ï´Ù.");
        else
        {

            var request = new UpdateUserTitleDisplayNameRequest() { DisplayName = NickName + "#" + GameManager.Instance.myPlayfabID };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSetAccountInfoSuccess, OnSetAccountInfoFailure);
        }
    }

    private void OnSetAccountInfoSuccess(UpdateUserTitleDisplayNameResult result)
    {
        GameManager.Instance.currentNickName = result.DisplayName.Split('#')[0];
        HideRenameButton();
    }

    private void OnSetAccountInfoFailure(PlayFabError error)
    {
        Debug.LogWarning("½ÇÆÐ");
        Debug.LogWarning(error.GenerateErrorReport());

    }
    ///
    ///
    ///


    public void ChangeNickName()
    {
        NameBox.text = NameBox.text.Trim();
        SetAccountInfo(NameBox.text);
    }
    public void ShowRenameButton() => RenameButton.SetActive(true);
    public void HideRenameButton() => RenameButton.SetActive(false);

    public void ShowOptionBox() => GameManager.Instance.OpenOption();
}
