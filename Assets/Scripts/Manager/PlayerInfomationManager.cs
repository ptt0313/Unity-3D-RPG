using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerInfomationManager : Singleton<PlayerInfomationManager>
{
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] public BasePlayerState playerState;
    [SerializeField] GameObject playerStateUI;
    [SerializeField] TextMeshProUGUI level;
    [SerializeField] TextMeshProUGUI exp;
    [SerializeField] TextMeshProUGUI hp;
    [SerializeField] TextMeshProUGUI stamina;
    [SerializeField] TextMeshProUGUI attackPoint;
    [SerializeField] TextMeshProUGUI defencePoint;
    [SerializeField] public Image weaponEquipment;
    [SerializeField] public Image ArmorEquipment;
    void Start()
    {
        goldText.text = playerState.gold.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpdateStat();
            bool isActive = !playerStateUI.activeSelf;
            playerStateUI.SetActive(isActive); // �κ��丮 UI Ȱ��ȭ/��Ȱ��ȭ ���
                                           // �κ��丮�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ǥ���ϰ�, �׷��� ������ ����ϴ�.
            Cursor.visible = isActive;
            // �κ��丮�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ����� �ʰ�, �׷��� ������ ��޴ϴ�.
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
        UpdateGold();
    }

    void UpdateGold()
    {
        goldText.text = playerState.gold.ToString();
    }
    void UpdateStat()
    {
        level.text = "���� : " + playerState.level.ToString();
        exp.text = "����ġ : " + playerState.currentExp + " / " + (playerState.maxExp * playerState.level * 2.5);
        hp.text = "ü�� : " + playerState.hp.ToString() + " / " + playerState.maxHp.ToString();
        stamina.text = "���¹̳� : " + playerState.maxStamina.ToString();
        attackPoint.text = "���ݷ� : " + playerState.attackPoint.ToString();
        defencePoint.text = "���� : " + playerState.defencePoint.ToString();
    }
}
