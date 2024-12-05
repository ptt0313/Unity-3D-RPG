using System.Collections.Generic;
using UnityEngine;
using System.IO;
using PlayFab.ClientModels;
using PlayFab;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private BasePlayerState playerState;
    [SerializeField] private Canvas canvas;

    private string saveFileName = "default.json"; // 기본 저장 파일 이름
    private string SaveFilePath => Path.Combine(Application.persistentDataPath, saveFileName);

    private bool mouseVisible;

    private void Start()
    {
        // 로그인 후 ID 가져오기
        if (!string.IsNullOrEmpty(PlayFabClientAPI.IsClientLoggedIn() ? PlayFabSettings.staticPlayer.PlayFabId : null))
        {
            saveFileName = PlayFabSettings.staticPlayer.PlayFabId + ".json";
            Debug.Log($"저장 파일 이름 설정: {saveFileName}");
        }
        else
        {
            Debug.LogWarning("PlayFab에 로그인이 필요합니다.");
        }
    }

    private void Update()
    {
        LevelUp();
        if (Input.GetMouseButtonDown(1))
        {
            mouseVisible = !mouseVisible;
            Cursor.visible = mouseVisible;
            Cursor.lockState = mouseVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!canvas.gameObject.activeSelf)
            {
                canvas.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }

    private void LevelUp()
    {
        if (playerState.currentExp >= playerState.maxExp)
        {
            playerState.currentExp -= playerState.maxExp;
            playerState.level += 1;
            playerState.attackPoint += 3;
            playerState.defencePoint += 3;
            playerState.maxHp += 5;
            playerState.maxStamina += 5;
            playerState.maxExp = playerState.level * 100;

            ParticleManager.Instance.ParticlePlay(2);
            SoundManager.Instance.PlayEffect("LevelUp");
        }
    }

    public void SavePlayerStateToPlayFab()
    {
        string json = playerState.ToJson(); // ScriptableObject 데이터를 JSON으로 직렬화
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { "PlayerState", json } }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("플레이어 데이터가 PlayFab에 저장되었습니다."),
            error => Debug.LogError("데이터 저장 실패: " + error.GenerateErrorReport()));
    }


    public void LoadPlayerStateFromPlayFab()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.LogError("PlayFab에 로그인이 되어 있지 않습니다.");
            return;
        }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), 
            result =>
            {
            if (result.Data != null && result.Data.ContainsKey("PlayerState"))
            {
                string json = result.Data["PlayerState"].Value;
                playerState.LoadFromJson(json);
                Debug.Log($"로드된 데이터: {json}"); // 로그 출력
                Debug.Log($"플레이어 상태 로드 완료: Level {playerState.level}, Gold {playerState.gold}, Items: {playerState.items.Count}");
            }
            else
            {
                Debug.LogWarning("저장된 플레이어 상태 데이터가 없습니다.");
            }
            },
            error =>
            {
            Debug.LogError($"플레이어 상태 로드 실패: {error.GenerateErrorReport()}");
            });
    }

    /// <summary>
    /// 플레이어 컴퓨터에 저장된 JSON 파일 삭제
    /// </summary>
    public void DeleteLocalPlayerState()
    {
        if (File.Exists(SaveFilePath))
        {
            File.Delete(SaveFilePath);
            Debug.Log($"로컬 JSON 파일 삭제 완료: {SaveFilePath}");
        }
        else
        {
            Debug.LogWarning("삭제할 로컬 JSON 파일이 없습니다.");
        }
    }

    /// <summary>
    /// PlayFab 서버에 저장된 데이터 삭제
    /// </summary>
    public void DeletePlayFabPlayerState()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.LogError("PlayFab에 로그인이 되어 있지 않습니다.");
            return;
        }

        var request = new UpdateUserDataRequest
        {
            KeysToRemove = new List<string> { "PlayerState" } // 삭제할 키 설정
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("PlayFab 서버에서 플레이어 상태 삭제 성공!"),
            error => Debug.LogError($"PlayFab 서버에서 플레이어 상태 삭제 실패: {error.GenerateErrorReport()}"));
    }
}
