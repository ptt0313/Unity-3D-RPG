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
                resultText.text = "회원가입 성공!";
            },
            error =>
            {
                resultText.text = "회원가입 실패: " + error.ErrorMessage;
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
                resultText.text = "로그인 성공!";
                loginSystem.SetActive(false);
                startBtn.SetActive(true);
                dataDeleteBtn.SetActive(true);
                signOutBtn.SetActive(true);

                // 로그인 성공 후 플레이어 상태 로드
                GameManager.Instance.LoadPlayerStateFromPlayFab();
            },
            error =>
            {
                resultText.text = "로그인 실패: " + error.ErrorMessage;
            });
    }

    public void SignOut()
    {
        PlayFabClientAPI.ForgetAllCredentials();

        Debug.Log("PlayFab 로그아웃 성공!");

        ResetUIAfterSignOut();
    }

    private void ResetUIAfterSignOut()
    {
        loginSystem.SetActive(true);
        startBtn.SetActive(false);
        dataDeleteBtn.SetActive(false);
        signOutBtn.SetActive(false);
        Debug.Log("UI 상태가 초기화");
    }
    
    
}
