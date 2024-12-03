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
                resultText.text = "회원가입 성공!";
            },
            error => {
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
            result => {
                resultText.text = "로그인 성공!";
                loginSystem.SetActive(false);
                startBtn.SetActive(true);
            },
            error => {
                resultText.text = "로그인 실패: " + error.ErrorMessage;
            }
        );
    }
    public void SavePlayerState(BasePlayerState playerState)
    {
        string json = playerState.ToJson(); // ScriptableObject를 JSON으로 변환

        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "PlayerState", json } // "PlayerState"라는 키에 JSON 저장
            }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("PlayerState 저장 성공!"),
            error => Debug.LogError($"PlayerState 저장 실패: {error.GenerateErrorReport()}"));
    }
    public void LoadPlayerState(BasePlayerState playerState)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), result =>
        {
            if (result.Data != null && result.Data.ContainsKey("PlayerState"))
            {
                string json = result.Data["PlayerState"].Value;
                Debug.Log($"PlayerState 데이터 로드 성공: {json}");

                playerState.LoadFromJson(json); // JSON을 ScriptableObject에 덮어쓰기
                Debug.Log($"PlayerState 로드 완료: Level {playerState.level}, HP {playerState.hp}");
            }
            else
            {
                Debug.Log("저장된 PlayerState 데이터가 없습니다.");
            }
        },
        error =>
        {
        Debug.LogError($"PlayerState 데이터 로드 실패: {error.GenerateErrorReport()}");
        });
    }
}
