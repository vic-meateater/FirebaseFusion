using System;
using Fusion;
using Fusion.Photon.Realtime;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Authorization : MonoBehaviour
{
    private const string AUTH_KEY = "player-unique-id";
    private const string LAST_LOGIN = "player-last-login";
    
    //[SerializeField] private string _playFabTitle;
    [SerializeField] private InputField _username;
    [SerializeField] private InputField _password;
    [SerializeField] private Button _signInButton;
    
    
    //private readonly string _customId = "TestAuth";
    private NetworkRunner _runner;

    private void Start()
    {
        // if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        //     PlayFabSettings.staticSettings.TitleId = _playFabTitle;
        //
        _signInButton.onClick.AddListener(Login);
        if (PlayerPrefs.HasKey(AUTH_KEY))
        {
            Debug.Log("PlayerPrefs.HasKey");
            var customId  = PlayerPrefs.GetString(AUTH_KEY, Guid.NewGuid().ToString());
            var request = new LoginWithCustomIDRequest
            {
                CustomId = customId,
                CreateAccount = false
            };
            PlayFabClientAPI.LoginWithCustomID(request,ResultCallback, 
                error => Debug.Log(error));
            PlayerPrefs.SetString(request.CustomId, DateTime.Now.ToString());
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            Debug.Log("Авторизируйтесь по паролю");
        }


        // Connect();
    }

    private void ResultCallback(LoginResult result)
    {
        Debug.Log(result.PlayFabId);
        Debug.Log(result.AuthenticationContext);
        Debug.Log(result.EntityToken);
        Debug.Log(result.SessionTicket);
        var token = result.EntityToken;
        var ticket = result.SessionTicket;
        
        
        //_runner.StartGame(new StartGameArgs() { });
        // Runner.Run(result);
        // NetworkObject
        // PhotonNetwork.AuthValues = new AuthenticationValues(result.PlayFabId);
        // PhotonNetwork.NickName = _customId;
        
        // PlayFabClientAPI.LinkCustomID(LinkCustomIDRequest(), result =>
        // {
        //     Debug.Log("Link successfully");
        // }, error =>
        // {
        //     Debug.Log("Error: " + error.ErrorMessage);
        // });
    }

    private void Login()
    {
       
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest()
        {
            Username = _username.text,
            Password = _password.text,
        }, 
            ResultCallback, 
            error =>
        {
            Debug.Log(error.ErrorMessage);
        });
    }

    private LinkCustomIDRequest LinkCustomIDRequest()
    {
        var request = new LinkCustomIDRequest();
        
        var hasKey = PlayerPrefs.HasKey(AUTH_KEY);
        if (!hasKey)
            PlayerPrefs.SetString(AUTH_KEY, Guid.NewGuid().ToString());
        
        request.CustomId = PlayerPrefs.GetString(AUTH_KEY, Guid.NewGuid().ToString());
        return request;
    }
    
    private void Connect()
    {
    }
}