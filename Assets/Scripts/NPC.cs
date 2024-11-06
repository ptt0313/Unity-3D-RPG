using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] GameObject interactionNPC;
    [SerializeField] TextMeshProUGUI _name;
    bool isActive;

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            interactionNPC.SetActive(isActive);
        }
    }
    private void Update()
    {
        _name.transform.rotation = Camera.main.transform.rotation;
        isActive = !interactionNPC.activeSelf;
        // UI�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ǥ���ϰ�, �׷��� ������ ����ϴ�.
        Cursor.visible = isActive;
        // UI�� Ȱ��ȭ�Ǹ� ���콺 Ŀ���� ����� �ʰ�, �׷��� ������ ��޴ϴ�.
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
