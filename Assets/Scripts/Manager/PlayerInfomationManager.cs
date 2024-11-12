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
    [SerializeField] public Image armorEquipment;
    void Start()
    {
        goldText.text = playerState.gold.ToString();
        if(playerState.currentWeapon != null)
        {
            weaponEquipment.sprite = playerState.currentWeapon.bigImage;
        }
        if(playerState.currentArmor != null)
        {
            armorEquipment.sprite = playerState.currentArmor.bigImage;
        }
    }

    void Update()
    {
        UpdateStat();
        if (Input.GetKeyDown(KeyCode.P))
        {
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
    public void UpdateStat()
    {
        level.text = "���� : " + playerState.level.ToString();
        exp.text = "����ġ : " + playerState.currentExp + " / " + (playerState.level * 100);
        hp.text = "ü�� : " + playerState.hp.ToString() + " / " + playerState.maxHp.ToString();
        stamina.text = "���¹̳� : " + playerState.stamina.ToString() + " / " + playerState.maxStamina.ToString();
        attackPoint.text = "���ݷ� : " + playerState.attackPoint.ToString();
        defencePoint.text = "���� : " + playerState.defencePoint.ToString();
    }
}
