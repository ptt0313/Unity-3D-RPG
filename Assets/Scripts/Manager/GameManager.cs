using System.Collections.Generic;
using UnityEngine;
using System.IO;
using PlayFab.ClientModels;
using PlayFab;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private BasePlayerState playerState;
    [SerializeField] private Canvas canvas;

    private string saveFileName = "default.json"; // �⺻ ���� ���� �̸�
    private string SaveFilePath => Path.Combine(Application.persistentDataPath, saveFileName);

    private bool mouseVisible;

    private void Start()
    {
        // �α��� �� ID ��������
        if (!string.IsNullOrEmpty(PlayFabClientAPI.IsClientLoggedIn() ? PlayFabSettings.staticPlayer.PlayFabId : null))
        {
            saveFileName = PlayFabSettings.staticPlayer.PlayFabId + ".json";
            Debug.Log($"���� ���� �̸� ����: {saveFileName}");
        }
        else
        {
            Debug.LogWarning("PlayFab�� �α����� �ʿ��մϴ�.");
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
        string json = playerState.ToJson(); // ScriptableObject �����͸� JSON���� ����ȭ
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { "PlayerState", json } }
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("�÷��̾� �����Ͱ� PlayFab�� ����Ǿ����ϴ�."),
            error => Debug.LogError("������ ���� ����: " + error.GenerateErrorReport()));
    }


    public void LoadPlayerStateFromPlayFab()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.LogError("PlayFab�� �α����� �Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), 
            result =>
            {
            if (result.Data != null && result.Data.ContainsKey("PlayerState"))
            {
                string json = result.Data["PlayerState"].Value;
                playerState.LoadFromJson(json);
                Debug.Log($"�ε�� ������: {json}"); // �α� ���
                Debug.Log($"�÷��̾� ���� �ε� �Ϸ�: Level {playerState.level}, Gold {playerState.gold}, Items: {playerState.items.Count}");
            }
            else
            {
                Debug.LogWarning("����� �÷��̾� ���� �����Ͱ� �����ϴ�.");
            }
            },
            error =>
            {
            Debug.LogError($"�÷��̾� ���� �ε� ����: {error.GenerateErrorReport()}");
            });
    }

    /// <summary>
    /// �÷��̾� ��ǻ�Ϳ� ����� JSON ���� ����
    /// </summary>
    public void DeleteLocalPlayerState()
    {
        if (File.Exists(SaveFilePath))
        {
            File.Delete(SaveFilePath);
            Debug.Log($"���� JSON ���� ���� �Ϸ�: {SaveFilePath}");
        }
        else
        {
            Debug.LogWarning("������ ���� JSON ������ �����ϴ�.");
        }
    }

    /// <summary>
    /// PlayFab ������ ����� ������ ����
    /// </summary>
    public void DeletePlayFabPlayerState()
    {
        if (!PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.LogError("PlayFab�� �α����� �Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        var request = new UpdateUserDataRequest
        {
            KeysToRemove = new List<string> { "PlayerState" } // ������ Ű ����
        };

        PlayFabClientAPI.UpdateUserData(request,
            result => Debug.Log("PlayFab �������� �÷��̾� ���� ���� ����!"),
            error => Debug.LogError($"PlayFab �������� �÷��̾� ���� ���� ����: {error.GenerateErrorReport()}"));
    }
}
