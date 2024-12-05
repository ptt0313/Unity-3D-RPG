using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System.IO;

public class PlayFabManager : MonoBehaviour
{
    [SerializeField] InputField idInputField;
    [SerializeField] InputField passwordInputField;
    [SerializeField] Text resultText;
    [SerializeField] GameObject loginSystem;
    [SerializeField] GameObject startBtn;
    [SerializeField] GameObject dataDeleteBtn;
    [SerializeField] GameObject signOutBtn;

    public void SignUp()
    {
        var registerRequest = new RegisterPlayFabUserRequest
        {
            Username = idInputField.text,
            Password = passwordInputField.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(registerRequest,
            result =>
            {
                resultText.text = "ȸ������ ����!";
            },
            error =>
            {
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
            result =>
            {
                resultText.text = "�α��� ����!";
                loginSystem.SetActive(false);
                startBtn.SetActive(true);
                dataDeleteBtn.SetActive(true);
                signOutBtn.SetActive(true);

                // �α��� ���� �� �÷��̾� ���� �ε�
                GameManager.Instance.LoadPlayerStateFromPlayFab();
            },
            error =>
            {
                resultText.text = "�α��� ����: " + error.ErrorMessage;
            });
    }

    public void SignOut()
    {
        PlayFabClientAPI.ForgetAllCredentials();

        Debug.Log("PlayFab �α׾ƿ� ����!");

        ResetUIAfterSignOut();
    }

    private void ResetUIAfterSignOut()
    {
        loginSystem.SetActive(true);
        startBtn.SetActive(false);
        dataDeleteBtn.SetActive(false);
        signOutBtn.SetActive(false);
        Debug.Log("UI ���°� �ʱ�ȭ");
    }
    
    
}
