using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayfabManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult loginResult)
    {
        Debug.Log("�α��� ����");

    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("�α��� ����");
        Debug.LogWarning(error.GenerateErrorReport());

        /*Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());*/
    }
}
