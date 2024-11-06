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
        // UI가 활성화되면 마우스 커서를 표시하고, 그렇지 않으면 숨깁니다.
        Cursor.visible = isActive;
        // UI가 활성화되면 마우스 커서를 잠그지 않고, 그렇지 않으면 잠급니다.
        Cursor.lockState = isActive ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
