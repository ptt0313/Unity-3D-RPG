using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] InputField idInputField;
    [SerializeField] InputField passwordInputField;
    [SerializeField] Text resultText;
    [SerializeField] GameObject loginSystem;
    [SerializeField] GameObject startBtn;
    public void SignUp()
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Username = idInputField.text,
            Password = passwordInputField.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest,
            result => {
                resultText.text = "ȸ������ ����!";
            },
            error => {
                resultText.text = "ȸ������ ����: " + error.ErrorMessage;
            }
        );
    }

    public void SignIn()
    {
        var loginRequest = new LoginWithPlayFabRequest
        {
            Username = idInputField.text,
            Password = passwordInputField.text
        };

        PlayFabClientAPI.LoginWithPlayFab(loginRequest,
            result => {
                resultText.text = "�α��� ����!";
                loginSystem.SetActive(false);
                startBtn.SetActive(true);
            },
            error => {
                resultText.text = "�α��� ����: " + error.ErrorMessage;
            }
        );
    }
    public void SavePlayerState(BasePlayerState playerState)
    {
        string json = playerState.ToJson(); // ScriptableObject�� JSON���� ��ȯ

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "PlayerState", json } // "PlayerState"��� Ű�� JSON ����
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("PlayerState ���� ����!"),
            error => Debug.LogError($"PlayerState ���� ����: {error.GenerateErrorReport()}"));
    }
    public void LoadPlayerState(BasePlayerState playerState)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null && result.Data.ContainsKey("PlayerState"))
            {
                string json = result.Data["PlayerState"].Value;
                Debug.Log($"PlayerState ������ �ε� ����: {json}");

                playerState.LoadFromJson(json); // JSON�� ScriptableObject�� �����
                Debug.Log($"PlayerState �ε� �Ϸ�: Level {playerState.level}, HP {playerState.hp}");
            }
            else
            {
                Debug.Log("����� PlayerState �����Ͱ� �����ϴ�.");
            }
        },
        error =>
        {
        Debug.LogError($"PlayerState ������ �ε� ����: {error.GenerateErrorReport()}");
        });
    }
}
