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
            playerStateUI.SetActive(isActive); // 인벤토리 UI 활성화/비활성화 토글
                                           // 인벤토리가 활성화되면 마우스 커서를 표시하고, 그렇지 않으면 숨깁니다.
            Cursor.visible = isActive;
            // 인벤토리가 활성화되면 마우스 커서를 잠그지 않고, 그렇지 않으면 잠급니다.
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
        level.text = "레벨 : " + playerState.level.ToString();
        exp.text = "경험치 : " + playerState.currentExp + " / " + (playerState.level * 100);
        hp.text = "체력 : " + playerState.hp.ToString() + " / " + playerState.maxHp.ToString();
        stamina.text = "스태미나 : " + playerState.stamina.ToString() + " / " + playerState.maxStamina.ToString();
        attackPoint.text = "공격력 : " + playerState.attackPoint.ToString();
        defencePoint.text = "방어력 : " + playerState.defencePoint.ToString();
    }
}
