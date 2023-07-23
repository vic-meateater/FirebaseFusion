using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;


public class ProfileManager : MonoBehaviour
{
    private const string LAST_LOGIN = "player-last-login";
    
    [SerializeField] private TextMeshProUGUI _usernameValue;
    [SerializeField] private TextMeshProUGUI _creationDateValue;
    [SerializeField] private TextMeshProUGUI _lastLoginValue;
    [SerializeField] private TextMeshProUGUI _errorLabel;

    private void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), resultCallback: result =>
        {
            _usernameValue.text = result.AccountInfo.Username;
            _creationDateValue.text = result.AccountInfo.Created.ToShortDateString();
            var lastLogin = PlayerPrefs.GetString(result.AccountInfo.CustomIdInfo.CustomId);
            _lastLoginValue.text = lastLogin;
        }, errorCallback: error =>
        {
            _errorLabel.text = error.ErrorMessage;
        });
    }
}
