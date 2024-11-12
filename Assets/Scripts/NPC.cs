using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject interactionNPC;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI _name;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if(Vector3.Distance(player.transform.position,gameObject.transform.position) <= 2 && Input.GetKeyDown(KeyCode.F))
        {
            bool isActive = !interactionNPC.activeSelf;

            interactionNPC.SetActive(isActive); // UI Ȱ��ȭ/��Ȱ��ȭ ���
                                                // UI�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ǥ���ϰ�, �׷��� ������ ����ϴ�.
            Cursor.visible = isActive;
            // UI�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ����� �ʰ�, �׷��� ������ ��޴ϴ�.
            Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
        }
        _name.transform.rotation = Camera.main.transform.rotation;
        
    }
}
